using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class Deck : MonoBehaviour
{
    [SerializeField] int deckSize = 48;
    [SerializeField] List<Word> deck = new List<Word>();

    [SerializeField] int substantiuSize = 8;
    [SerializeField] int adjSize = 8;
    [SerializeField] int vSize = 8;
    [SerializeField] int advSize = 8;
    [SerializeField] int artSize = 8;
    [SerializeField] int prnSize = 8;

    // Start is called before the first frame update
    void Start()
    {
        GetWordsByType(WORD_TYPES.SUBSTANTIU, substantiuSize);
        GetWordsByType(WORD_TYPES.ADJECTIU, adjSize);
        GetWordsByType(WORD_TYPES.VERB, vSize);
        GetWordsByType(WORD_TYPES.ADVERBI, advSize);
        GetWordsByType(WORD_TYPES.ARTICLE, artSize);
        GetWordsByType(WORD_TYPES.PRONOM, prnSize);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void GetWordsByType(WORD_TYPES type, int size)
    {
        for (int i = 0; i < size; i++)
        {
            //deck.Add();
        }
    }

    public void ShuffleDeck() //This function must be called only after the full deck has been created.
    {
        //For each card in deck we change its position
        for (int i = 0; i < deck.Count - 1; ++i)
        {
            int r = UnityEngine.Random.Range(i, deck.Count);
            var tmp = deck[i];
            deck[i] = deck[r];
            deck[r] = tmp;
        }
    }
}
