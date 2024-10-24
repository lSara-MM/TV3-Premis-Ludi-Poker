using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using UnityEngine;

public class PlayCards : MonoBehaviour
{
    [SerializeField] GameObject playedCards;
    [SerializeField] GameObject handCards;
    private Word wordRef;
    // Start is called before the first frame update
    void Start()
    {
        wordRef = new Word();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnButtonPress()
    {
        // First, we add on a list all cards that are in play (the children of the children of "playedCards" (inside Card Slot)) to be able to read them 
        List<GameObject> listplayedCards = new List<GameObject>();

        for (int i = 0; i < playedCards.transform.childCount; i++)
        {
            listplayedCards.Add(playedCards.transform.GetChild(i).GetChild(0).gameObject); // This gives a list with the the current slots
        }

        int numberValidatedCards = 0;
        int numberEqualCards = 0;
        for (int i = 0; i < listplayedCards.Count; i++)
        {
            //Debug.Log(listplayedCards[i].name);
            if (i + 1 < listplayedCards.Count) // Avoid accesing out of bounds of the list
            {
                if (listplayedCards[i].GetComponent<WordBehaviour>().word.Validate(listplayedCards[i + 1].GetComponent<WordBehaviour>().word.type)) // Check if the next word is of a valid type
                {
                    numberValidatedCards++;
                }
                else if(listplayedCards[i].GetComponent<WordBehaviour>().word.Same(listplayedCards[i + 1].GetComponent<WordBehaviour>().word.type)) 
                {
                    numberEqualCards++;
                }
            }
        }
        UnityEngine.Debug.Log(numberValidatedCards*100 + numberEqualCards*5);

        handCards.GetComponent<HorizontalCardHolder>().CreateHand(); // Create new hand after playing
    }
}
