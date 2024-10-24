using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DiscardCards : MonoBehaviour
{
    [SerializeField] private GameObject handCards;
    public int numberDiscards;
    [SerializeField] private GameObject numberUI;

    
    // Start is called before the first frame update
    void Start()
    {
        numberUI.GetComponent<TextMeshProUGUI>().text = numberDiscards.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnButtonPress() // Wrapper for couroutine (can not call one from the Button component)
    {
        StartCoroutine(Discard());
    }

    private IEnumerator Discard()
    {
        numberDiscards--;
        numberUI.GetComponent<TextMeshProUGUI>().text = numberDiscards.ToString();

        gameObject.GetComponent<Button>().interactable = false; // Do not allow player to interact with the button while discarding the hand

        yield return handCards.GetComponent<HorizontalCardHolder>().DeleteHand(); // Delay to make smooth

        handCards.GetComponent<HorizontalCardHolder>().CreateHand();

        //if (handCards.GetComponent<HorizontalCardHolder>().selectedCards.Count != 0)
        //{
        //    gameObject.GetComponent<Button>().interactable = true;
        //}
    }
}
