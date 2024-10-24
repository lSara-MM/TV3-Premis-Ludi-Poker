using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using DG.Tweening;
using System.Linq;

public class HorizontalCardHolder : MonoBehaviour
{

    [SerializeField] public Card selectedCard;
    [SerializeReference] private Card hoveredCard;

    [SerializeField] private GameObject slotPrefab;
    private RectTransform rect;

    [Header("Spawn Settings")]
    [SerializeField] private int cardsToSpawn = 7;
    public List<Card> cards;

    bool isCrossing = false;
    [SerializeField] private bool tweenCardReturn = true;

    // Play Area
    public GameObject otherArea;

    // Deck
    public GameObject deck;

    void Start()
    {
        if (cardsToSpawn != 0)
        {
            Invoke("CreateHand", 0.1f); // Delay, Deck doesn't exist if called at the same time
        }
    }

    private void BeginDrag(Card card)
    {
        selectedCard = card;
    }


    void EndDrag(Card card)
    {
        if (selectedCard == null)
            return;

        if (otherArea.GetComponent<AreaHandler>().isHovering && !otherArea.GetComponent<HorizontalCardHolder>().cards.Contains(selectedCard))
        {
            //selectedCard.transform.parent.transform.SetParent(playArea.transform);
            //cards.Remove(selectedCard);
            //playArea.GetComponent<HorizontalCardHolder>().cards.Add(selectedCard);

            // "Move" card to the other area, just reparenting doesn't work
            otherArea.GetComponent<HorizontalCardHolder>().AddCard(selectedCard);

            Destroy(selectedCard.transform.parent.gameObject);
            cards.Remove(selectedCard);
        }
        else
        {
            selectedCard.transform.DOLocalMove(selectedCard.selected ? new Vector3(0, selectedCard.selectionOffset, 0) : Vector3.zero, tweenCardReturn ? .15f : 0).SetEase(Ease.OutBack);

            rect.sizeDelta += Vector2.right;
            rect.sizeDelta -= Vector2.right;

            selectedCard = null;
        }
    }

    void CardPointerEnter(Card card)
    {
        hoveredCard = card;
    }

    void CardPointerExit(Card card)
    {
        hoveredCard = null;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Delete))
        {
            if (hoveredCard != null)
            {
                Destroy(hoveredCard.transform.parent.gameObject);
                cards.Remove(hoveredCard);

            }
        }

        if (Input.GetMouseButtonDown(1))
        {
            foreach (Card card in cards)
            {
                card.Deselect();
            }
        }

        if (selectedCard == null)
            return;

        if (isCrossing)
            return;

        for (int i = 0; i < cards.Count; i++)
        {

            if (selectedCard.transform.position.x > cards[i].transform.position.x)
            {
                if (selectedCard.ParentIndex() < cards[i].ParentIndex())
                {
                    Swap(i);
                    break;
                }
            }

            if (selectedCard.transform.position.x < cards[i].transform.position.x)
            {
                if (selectedCard.ParentIndex() > cards[i].ParentIndex())
                {
                    Swap(i);
                    break;
                }
            }
        }
    }

    void Swap(int index)
    {
        isCrossing = true;

        Transform focusedParent = selectedCard.transform.parent;
        Transform crossedParent = cards[index].transform.parent;

        cards[index].transform.SetParent(focusedParent);
        cards[index].transform.localPosition = cards[index].selected ? new Vector3(0, cards[index].selectionOffset, 0) : Vector3.zero;
        selectedCard.transform.SetParent(crossedParent);

        isCrossing = false;

        if (cards[index].cardVisual == null)
            return;

        bool swapIsRight = cards[index].ParentIndex() > selectedCard.ParentIndex();
        cards[index].cardVisual.Swap(swapIsRight ? -1 : 1);

        //Updated Visual Indexes
        foreach (Card card in cards)
        {
            card.cardVisual.UpdateIndex(transform.childCount);
        }
    }

    // Create card when moving from one area to another
    public void AddCard(Card cardCopied)
    {
        GameObject newCard = Instantiate(slotPrefab, transform);

        cards.Add(newCard.GetComponentInChildren<Card>());

        newCard.GetComponentInChildren<Card>().PointerEnterEvent.AddListener(CardPointerEnter);
        newCard.GetComponentInChildren<Card>().PointerExitEvent.AddListener(CardPointerExit);
        newCard.GetComponentInChildren<Card>().BeginDragEvent.AddListener(BeginDrag);
        newCard.GetComponentInChildren<Card>().EndDragEvent.AddListener(EndDrag);

        // Add all values from the copied card to the new cone created
        newCard.GetComponentInChildren<Card>().name = cardCopied.gameObject.name;
        newCard.GetComponentInChildren<WordBehaviour>().word = cardCopied.gameObject.GetComponent<WordBehaviour>().word;

        StartCoroutine(Frame());

        IEnumerator Frame()
        {
            yield return new WaitForSecondsRealtime(.1f);

            if (newCard.GetComponentInChildren<Card>().cardVisual != null)
            {
                newCard.GetComponentInChildren<Card>().cardVisual.UpdateIndex(transform.childCount);
            }
        }
    }
    public void CreateHand() // Don't call if deck is empty
    {
        int spawn = cardsToSpawn - transform.childCount;

        if (spawn > deck.GetComponent<Deck>().currentDeck.Count) // Check if cards needed to create hand is greater than the current deck
        {
            spawn = deck.GetComponent<Deck>().currentDeck.Count;
        }

        for (int i = 0; i < spawn; i++)
        {
            Instantiate(slotPrefab, transform);
        }

        rect = GetComponent<RectTransform>();
        cards = GetComponentsInChildren<Card>().ToList();

        List<Card> newCards = new List<Card>();

        for (int i = cards.Count - spawn; i < cards.Count; i++)
        {
            newCards.Add(cards[i]); 
        }

        int cardCount = 0;

        foreach (Card card in newCards)
        {
            card.PointerEnterEvent.AddListener(CardPointerEnter);
            card.PointerExitEvent.AddListener(CardPointerExit);
            card.BeginDragEvent.AddListener(BeginDrag);
            card.EndDragEvent.AddListener(EndDrag);
            cardCount++;

            // Add word to the card
            card.gameObject.GetComponent<WordBehaviour>().word = deck.GetComponent<Deck>().currentDeck[0]; // Depending on how this is made if we need to assign the type of word first the random word could be assiged using [random + (int)WORD_TYPE * numWordsCategory /*16 in this case*/]
            deck.GetComponent<Deck>().currentDeck.RemoveAt(0);

            card.name = card.gameObject.GetComponent<WordBehaviour>().word.word;
        }

        StartCoroutine(Frame());

        IEnumerator Frame()
        {
            yield return new WaitForSecondsRealtime(.1f);
            for (int i = 0; i < newCards.Count; i++)
            {
                if (newCards[i].cardVisual != null)
                    newCards[i].cardVisual.UpdateIndex(transform.childCount);
            }
        }
    }
}
