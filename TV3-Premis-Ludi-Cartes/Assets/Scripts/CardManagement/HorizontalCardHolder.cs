using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using DG.Tweening;
using System.Linq;
using UnityEditor.PackageManager;
using UnityEngine.UI;

public class HorizontalCardHolder : MonoBehaviour
{

    [SerializeField] public Card selectedCard;
    [SerializeReference] private Card hoveredCard;

    [SerializeField] private GameObject slotPrefab;
    private RectTransform rect;

    [Header("Spawn Settings")]
    [SerializeField] private int cardsToSpawn = 8;
    public List<Card> cards;

    bool isCrossing = false;
    [SerializeField] private bool tweenCardReturn = true;

    // Other Area to check if mouse is inside to change card area
    public GameObject otherArea;

    // Deck
    public GameObject deck;
    [SerializeField] float delay;

    // Manage selected cards
    public List<Card> listSelectedCards;
    public bool manageDiscard = false;
    public Button discardButton;
    public Button playButton;

    void Start()
    {
        rect = GetComponent<RectTransform>();
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

        if (otherArea.GetComponent<AreaHandler>().isHovering && !otherArea.GetComponentInChildren<HorizontalCardHolder>().cards.Contains(selectedCard))
        {
            //selectedCard.transform.parent.transform.SetParent(playArea.transform);
            //cards.Remove(selectedCard);
            //playArea.GetComponent<HorizontalCardHolder>().cards.Add(selectedCard);

            // "Move" card to the other area, just reparenting does not work
            selectedCard.selected = false;

            // Check if discard button should be interactable
            if (manageDiscard)
            {
                listSelectedCards.Remove(selectedCard);
                if (listSelectedCards.Count == 0)
                {
                    discardButton.interactable = false;
                }
            }

            otherArea.GetComponentInChildren<HorizontalCardHolder>().AddCard(selectedCard);

            Destroy(selectedCard.transform.parent.gameObject);
            cards.Remove(selectedCard);

            // Check if play button should be interactable
            playButton.GetComponent<PlayCards>().CheckInteractable();
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
        // Check how many cards are needed to have a full hand
        int spawn = cardsToSpawn - transform.childCount - otherArea.GetComponentInChildren<HorizontalCardHolder>().cards.Count;

        if (spawn > deck.GetComponent<Deck>().playerDeck.Count) // Check if cards needed to create hand is greater than the current deck
        {
            spawn = deck.GetComponent<Deck>().playerDeck.Count;
        }

        for (int i = 0; i < spawn; i++)
        {
            Instantiate(slotPrefab, transform);
        }

        cards = GetComponentsInChildren<Card>().ToList(); // Reference to all the cards in the player hand (new + old cards)

        List<Card> newCards = new List<Card>(); // List to manage the recently created cards

        if (spawn != cardsToSpawn)
        {
            for (int i = cards.Count - spawn; i < cards.Count; i++) // Check which new cards have been created
            {
                newCards.Add(cards[i]);
            }
        }
        else // If the number of cards created is equal to the current player hand, there are no new cards so it is equal to the card list
        {
            newCards = cards;
        }

        int cardCount = 0;

        // Only create events and add word for the new created cards
        foreach (Card card in newCards)
        {
            // Add events for the fancy effects
            card.PointerEnterEvent.AddListener(CardPointerEnter);
            card.PointerExitEvent.AddListener(CardPointerExit);
            card.BeginDragEvent.AddListener(BeginDrag);
            card.EndDragEvent.AddListener(EndDrag);
            cardCount++;

            // Add word to the card from the current deck, delete the word from the deck
            card.gameObject.GetComponent<WordBehaviour>().word = deck.GetComponent<Deck>().playerDeck[0];
            deck.GetComponent<Deck>().playerDeck.RemoveAt(0);

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

    public IEnumerator DeleteCardList(List<Card> listCards)
    {
        foreach (Card card in listCards.ToList())
        {
            Destroy(card.transform.parent.gameObject);
            cards.Remove(card);

            if (listSelectedCards.Contains(card)) // Clean card from selected list, bugs discard button otherwise
            {
                listSelectedCards.Remove(card);
            }

            yield return new WaitForSeconds(delay);
        }
    }
}
