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

    // Start is called before the first frame update
    void Start()
    {
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

        int numberValidatedCards = 0;
        int numberEqualCards = 0;


        bool lastWasVal = false, lastWasSame = false;

        for (int i = 0; i < listplayedCards.Count; i++)
        {
            if (i + 1 < listplayedCards.Count) // Avoid accesing out of bounds of the list
            {
                if (listplayedCards[i].Validate(listplayedCards[i + 1].type))
                {
                    lastWasVal = true;
                    if (lastWasSame)
                    {
                        lastWasSame = false;
                    }
                    else
                    {
                        UnityEngine.Debug.Log(listplayedCards[i].word + " con " + listplayedCards[i + 1].word);
                        numberValidatedCards++;
                    }
                }
                else if (listplayedCards[i].Same(listplayedCards[i + 1].type))
                {
                    lastWasSame = true;
                    if (lastWasVal)
                    {
                        lastWasVal = false;
                    }
                    else
                    {
                        UnityEngine.Debug.Log(listplayedCards[i].word + " = " + listplayedCards[i + 1].word);
                        numberEqualCards++;
                    }
                }
                else 
                {
                    lastWasVal = false;
                    lastWasSame = false;

                }
            }
        }

        UnityEngine.Debug.Log($"{numberValidatedCards} + {numberEqualCards}");
        gameManager.CalculateScore(numberValidatedCards, numberEqualCards);


        handCards.GetComponent<HorizontalCardHolder>().CreateHand(); // Create new hand after playing

        numberPlays--;
        numberUI.GetComponent<TextMeshProUGUI>().text = numberPlays.ToString();

        if (numberPlays == 0)
        {
            gameObject.GetComponent<Button>().interactable = false;
        }
    }

    public int CheckSentenceCombo(List<Word> listplayedCards, int start = 0)
    {
        int i = start;
        for (i = start; i < listplayedCards.Count; i++)
        {
            //Debug.Log(listplayedCards[i].name);
            if (i + 1 < listplayedCards.Count) // Avoid accesing out of bounds of the list
            {
                if (!listplayedCards[i].Validate(listplayedCards[i + 1].type)) // Check if the next word is of a valid type
                {
                    break;
                }
            }
        }

        return i;
    }

    public int CheckSameTypeCombo(List<Word> listplayedCards, int start)
    {
        int i = start;
        for (i = start; i < listplayedCards.Count; i++)
        {
            //Debug.Log(listplayedCards[i].name);
            if (i + 1 < listplayedCards.Count) // Avoid accesing out of bounds of the list
            {
                if (!listplayedCards[i].Same(listplayedCards[i + 1].type)) // Check if the next word is of a valid type
                {
                    break;
                }
            }
        }

        return i;
    }
}
