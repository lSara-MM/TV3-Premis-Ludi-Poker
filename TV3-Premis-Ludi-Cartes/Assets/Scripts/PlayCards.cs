using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class PlayCards : MonoBehaviour
{
    public GameObject playedCards;
    [SerializeField] GameObject handCards;

    // Start is called before the first frame update
    void Start()
    {

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

        do
        {
            numberValidatedCards = CheckSentenceCombo(listplayedCards, numberEqualCards);
            numberEqualCards = CheckSameTypeCombo(listplayedCards, numberValidatedCards);

        } while (numberValidatedCards + numberEqualCards < listplayedCards.Count);

        {//for (int i = 0; i < listplayedCards.Count; i++)
        //{
        //    //Debug.Log(listplayedCards[i].name);
        //    if (i + 1 < listplayedCards.Count) // Avoid accesing out of bounds of the list
        //    {
        //        if (listplayedCards[i].GetComponent<WordBehaviour>().word.Validate(listplayedCards[i + 1].GetComponent<WordBehaviour>().word.type)) // Check if the next word is of a valid type
        //        {
        //            numberValidatedCards++;
        //        }
        //        else if (listplayedCards[i].GetComponent<WordBehaviour>().word.Same(listplayedCards[i + 1].GetComponent<WordBehaviour>().word.type))
        //        {
        //            numberEqualCards++;
        //        }
        //    }
        //}
        }

        UnityEngine.Debug.Log(numberValidatedCards * 100 + numberEqualCards * 5);
        handCards.GetComponent<HorizontalCardHolder>().CreateHand(); // Create new hand after playing
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
