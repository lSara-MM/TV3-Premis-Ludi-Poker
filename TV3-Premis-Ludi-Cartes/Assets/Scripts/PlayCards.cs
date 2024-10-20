using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayCards : MonoBehaviour
{
    [SerializeField] GameObject playedCards;
    [SerializeField] GameObject handCards;
    // Start is called before the first frame update
    void Start()
    {
        
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
        
        // Temporal
        for (int i = 0; i < listplayedCards.Count; i++) 
        {
            Debug.Log(listplayedCards[i].name);
        }

        handCards.GetComponent<HorizontalCardHolder>().CreateHand();
    }
}
