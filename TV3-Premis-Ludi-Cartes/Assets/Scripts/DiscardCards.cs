using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DiscardCards : MonoBehaviour
{
    [SerializeField] GameObject handCards;
    
    // Start is called before the first frame update
    void Start()
    {
        
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
        gameObject.GetComponent<Button>().interactable = false; // Do not allow player to interact with the button while discarding the hand

        yield return handCards.GetComponent<HorizontalCardHolder>().DeleteHand(); // Delay to make smooth

        handCards.GetComponent<HorizontalCardHolder>().CreateHand();
        gameObject.GetComponent<Button>().interactable = true;
    }
}
