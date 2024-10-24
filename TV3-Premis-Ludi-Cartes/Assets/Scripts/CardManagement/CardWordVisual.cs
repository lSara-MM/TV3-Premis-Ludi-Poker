using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CardWordVisual : MonoBehaviour
{
    public Card parentCard;
    public TMP_Text tmp_text;

    // Start is called before the first frame update
    void Start()
    {
        parentCard = this.gameObject.GetComponent<CardVisual>().parentCard;
        tmp_text.text = parentCard.name;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
