using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayCards : MonoBehaviour
{
    public GameObject playedCards;
    [SerializeField] GameObject handCards;

    [SerializeField] private int numberPlays;
    [SerializeField] private GameObject numberUI;

    [SerializeField] private GameManager gameManager; //We need access to the script to call the function and add the score

    [SerializeField] private TextMeshProUGUI currentScore;

    // Start is called before the first frame update
    void Start()
    {
        numberPlays = GameObject.FindWithTag("Data").GetComponent<GenerateData>().numPlays;
        numberUI.GetComponent<TextMeshProUGUI>().text = numberPlays.ToString();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ValidatePlay()
    {
        // First, we add on a list all cards that are in play (the children of the children of "playedCards" (inside Card Slot)) to be able to read them 
        List<Word> listplayedCards = new List<Word>();

        for (int i = 0; i < playedCards.transform.childCount; i++)
        {
            listplayedCards.Add(playedCards.transform.GetChild(i).GetChild(0).gameObject.GetComponent<WordBehaviour>().word); // This gives a list with the the current slots
        }

        gameManager.CalculateScore(CombosSeparator(listplayedCards));

        currentScore.text = "+ 0";

        //// Delete played cards if game didn't finish 
        //if (!gameManager.CheckEndGame())
        //{
        //    StartCoroutine(DeletePlayed());
        //}
    }

    List<List<Word>> CombosSeparator(List<Word> play)
    {
        bool validating = false, repeating = false; //Detect the punctuation method used
        List<Word> currentCombo = new List<Word>(); //Word list to add to combos

        List<List<Word>> combos = new List<List<Word>>(); //Here each group of word that score together will be saved.

        for (int i = 0; i < play.Count; i++)
        {
            if (i < play.Count - 1)
            {
                if (play[i].Validate(play[i + 1].type) && !repeating) //If next word is valid but we weren't checking for equals add word to list of current combo
                {
                    currentCombo.Add(play[i]);
                    validating = true;
                }
                else if (play[i].Same(play[i + 1].type) && !validating)
                {
                    currentCombo.Add(play[i]);
                    repeating = true;
                }
                else if (validating || repeating)
                {
                    currentCombo.Add(play[i]);
                    combos.Add(currentCombo);

                    //We reset the values to start checking again
                    validating = repeating = false;
                    currentCombo = new List<Word>();
                }
            }
            else if (validating || repeating) //This case is necesary to not access out of range of the list of words
            {
                currentCombo.Add(play[i]);
                combos.Add(currentCombo);
            }
        }

        return combos;
    }

    // Disable button if there are no played cards, otherwise enable it
    public void CheckInteractable()
    {
        currentScore.text = "+ 0";

        if (playedCards.GetComponent<HorizontalCardHolder>().cards.Count == 0 || numberPlays == 0)
        {
            gameObject.GetComponent<Button>().interactable = false;
        }
        else
        {
            gameObject.GetComponent<Button>().interactable = true;

            if (playedCards.GetComponent<HorizontalCardHolder>().cards.Count == 1)
            {
                currentScore.text = "+ 0";
            }
            else
            {
                List<Word> listplayed = new List<Word>();

                for (int i = 0; i < playedCards.transform.childCount; i++)
                {
                    listplayed.Add(playedCards.transform.GetChild(i).GetChild(0).gameObject.GetComponent<WordBehaviour>().word);
                }

                currentScore.text = "+ " + gameManager.CurrentScore(CombosSeparator(listplayed)).ToString();
            }
        }
    }

    // Delete played cards
    public IEnumerator DeletePlayed()
    {
        gameObject.GetComponent<Button>().interactable = false; // Do not allow player to interact with the button while calculating score the hand

        // Function in coroutine to make it smoother
        yield return playedCards.GetComponent<HorizontalCardHolder>().DeleteCardList(playedCards.GetComponent<HorizontalCardHolder>().cards);

        // Create new hand after playing
        handCards.GetComponent<HorizontalCardHolder>().CreateHand();

        // Manage UI Play Button
        if (numberPlays != 0)
        {
            numberPlays--;
            numberUI.GetComponent<TextMeshProUGUI>().text = numberPlays.ToString();
        }
        else
        {
            gameObject.GetComponent<Button>().interactable = false;
        }
    }

    public int GetNumberPlays() {  return numberPlays; }
}
