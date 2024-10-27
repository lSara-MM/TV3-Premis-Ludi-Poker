using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PrintComboOptions : MonoBehaviour
{
    public GameObject wordTypes;
    private List<GameObject> pictures = new List<GameObject>();
    public TMP_Dropdown dropdown;

    Word wordCkeck = new Word();

    // Start is called before the first frame update
    void Start()
    {
        dropdown = this.gameObject.GetComponent<TMP_Dropdown>();

        for (int i = 0; i < wordTypes.transform.childCount; i++) 
        { 
            pictures.Add(wordTypes.transform.GetChild(i).gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GetDropdowValue() 
    {
        //Reset shown cards
        foreach(GameObject obj in pictures) 
        {
            obj.transform.position= Vector3.zero;
            obj.SetActive(false);
        }
        
        //Here the pictures to show will be saved.
        List<GameObject> goToShow = new List<GameObject>();
        int pickedEntryIndex = dropdown.value;

        //Generic set

        wordCkeck = new Word("", (WORD_TYPES)pickedEntryIndex);
        //wordCkeck.type = (WORD_TYPES)pickedEntryIndex;
        for (int i = 0; i < wordTypes.transform.childCount; i++)
        {
            //If is the same card type move to the side
            if((WORD_TYPES)i == wordCkeck.type) 
            {
                pictures[i].gameObject.SetActive(true);
                pictures[i].gameObject.GetComponent<RectTransform>().anchoredPosition = new Vector3(-1000,0,0); //If its the current type move to position
            }

            //If is a valid combination put on a list to latter show and move them.
            if (wordCkeck.Validate((WORD_TYPES)i))
            {
                goToShow.Add(pictures[i]);
            }
        }

        for (int i = 0;i<goToShow.Count;i++) 
        {
            goToShow[i].gameObject.SetActive(true);
            goToShow[i].gameObject.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, -170 * i,0) ;
        }

    }


}
