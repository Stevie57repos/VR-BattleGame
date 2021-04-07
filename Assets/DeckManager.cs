using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeckManager : MonoBehaviour
{
    [SerializeField] private GameObject _cardPrefab;
    [SerializeField] private DeckScriptableObject deckData;

	public List<CardScriptableObject> deck;
	public List<CardScriptableObject> hand;
	public List<CardScriptableObject> graveyard;
	public List<CardScriptableObject> burn;

	private int maxHandSize = 5;

	public void Start()
    {
		LoadCards();
        ShuffleDeck();
        Draw(3);
    }

	void LoadCards()
    {
		foreach (CardScriptableObject card in deckData.cards)
        {
			this.deck.Add(card);
        }
    }

	public void ShuffleDeck()
    {
		for(int t = 0; t < deck.Count; t++)
        {
            CardScriptableObject tmp = deck[t];
            int r = Random.Range(t, deck.Count);
            deck[t] = deck[r];
            deck[r] = tmp;
        }
    }

    public CardScriptableObject GetNextCard()
    {
        return deck[0];
    }

    public void Draw(int n =1)
    {
        for(int i = 0; i < n; i++)
        {
            // check if deck is empty
            if(deck.Count == 0)
            {
                Debug.Log("Deck has run out of cards");
                RefillDeck();
            }
            else
            {
                // check if there is still room in the hand
                CardScriptableObject card = GetNextCard();
                if (hand.Count > maxHandSize)
                {
                    Overdraw(card);
                }
                else
                {
                    // draw the card
                    Debug.Log("Drew a " + card.name + " card!");
                    SpawnCard(card);
                    hand.Add(card);
                    deck.Remove(card);
                }
            }
        }
        FanCards();
    }

    private void FanCards()
    {
       // TODO : for each card in hand , change their transform
        
    }

    private void SpawnCard(CardScriptableObject card)
    {
        GameObject cardObject = Instantiate(_cardPrefab);

        // TODO : Set this to card controller
        cardObject.GetComponent<CardGameObject>().cardData = card;
    }

    private void RefillDeck()
    {
        foreach(CardScriptableObject card in graveyard)
        {
            deck.Add(card);
        }
        graveyard.Clear();
        ShuffleDeck();
    }

    private void Overdraw(CardScriptableObject card)
    {
        Debug.Log("Overdrew a " + card.name + " card! It was sent to the graveyard!");
        graveyard.Add(card);
        deck.Remove(card);
    }

    public void Burn(CardScriptableObject card)
    {
        Debug.Log("Burned a " + card.name + " card! It was sent to the burned pile It will not be refilled!");
        burn.Add(card);
        hand.Remove(card);
    }
}
