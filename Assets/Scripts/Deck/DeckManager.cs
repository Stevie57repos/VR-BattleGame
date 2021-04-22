using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeckManager : MonoBehaviour
{
    public GameObject CardspawnSpotGO;
    private GameManager_BS gameManager;

    [SerializeField] private GameObject _cardPrefab;
    [SerializeField] private DeckScriptableObject _deckData;

	public List<CardScriptableObject> deck;
	public List<CardScriptableObject> hand;
	public List<CardScriptableObject> graveyard;
	public List<CardScriptableObject> burn;

    public List<GameObject> handCards;
    float cardSpreadDistance = 0f;

	private int maxHandSize = 5;

    [SerializeField] GameManagerEventChannelSO _gameManagerBattleStart;
    [SerializeField] private CardEffectEventChannelSO _cardEffectEvent;

    private void OnEnable()
    {
        _gameManagerBattleStart.GameManagerEvent += DeckStart;
        _cardEffectEvent.OnCardEffectActivate += NewTurnV2;
    }

    private void OnDisable()
    {
        _gameManagerBattleStart.GameManagerEvent -= DeckStart;
        _cardEffectEvent.OnCardEffectActivate -= NewTurnV2;
    }

    private void Start()
    {

    }

    void DeckStart()
    {
        LoadCards();
        ShuffleDeck();
        Draw(3);
    }

	void LoadCards()
    {
		foreach (CardScriptableObject card in _deckData.cards)
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
                //Debug.Log("drawing card");
                if (hand.Count > maxHandSize)
                {
                    Overdraw(card);
                }
                else
                {
                    // draw the card
                    //Debug.Log("Drew a " + card.name + " card!");
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
        for (int i = 0; i < handCards.Count; i++)
        {
            var CardspawnPos = CardspawnSpotGO.transform.position;
            Vector3 cardSpot = new Vector3((cardSpreadDistance + CardspawnPos.x), CardspawnPos.y, CardspawnPos.z);
            handCards[i].transform.position = cardSpot;
            cardSpreadDistance = cardSpreadDistance + 0.25f;
        }
        cardSpreadDistance = 0f;
    }
    public void CardSelected(GameObject SelectedCard)
    {
        foreach (GameObject card in handCards)
        {
            if(SelectedCard.gameObject != card)
            {
                card.SetActive(false);
            }
        }
    }

    public void CardUnselected()
    {
        foreach (GameObject card in handCards)
        {
            card.SetActive(true);
        }
        FanCards();
    }

    private void SpawnCard(CardScriptableObject card)
    {
        GameObject cardObject = Instantiate(_cardPrefab);
        // TODO : Set this to card controller
        var cardController = cardObject.GetComponent<CardController>();
        cardController.CardData = card;
        cardController.DeckManager = this;
        handCards.Add(cardObject);
    }

    private void RefillDeck()
    {
        foreach(CardScriptableObject card in graveyard)
        {
            deck.Add(card);
        }
        graveyard.Clear();
        ShuffleDeck();
        Draw();
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

    private void NewTurnV2(GameObject cardObject, CardScriptableObject cardData)
    {
        UpdateCardLists(cardObject, cardData);
        CardUnselected();
        Draw();
    }

    public void UpdateCardLists(GameObject cardGO, CardScriptableObject card)
    {
        Debug.Log("Played a " + card.name + " card!");
        graveyard.Add(card);
        hand.Remove(card);
        handCards.Remove(cardGO);
    }

    //private void NewTurn()
    //{
    //    CardUnselected();
    //    Draw();
    //}

    //public void NewTurn_Sword()
    //{
    //    UpdateCardLists(GameEventsHub.SwordDamage.CardGO, GameEventsHub.SwordDamage.CardSO);
    //    NewTurn();
    //}

    //public void NewTurn_Shield()
    //{
    //    UpdateCardLists(GameEventsHub.ShieldDestroyed.CardGO, GameEventsHub.ShieldDestroyed.CardSO);
    //    NewTurn();
    //}

    //public void NewTurn_SpellDamage()
    //{
    //    UpdateCardLists(GameEventsHub.SpellDamage.CardGO, GameEventsHub.SpellDamage.CardSO);
    //    NewTurn();
    //}

    //public void SpellHealEvent(GameObject cardObject, CardScriptableObject cardData)
    //{
    //    UpdateCardLists(cardObject, cardData);
    //    NewTurn();
    //}

    //public void SpellDamageEvent(GameObject cardObject, CardScriptableObject cardData)
    //{
    //    UpdateCardLists(cardObject, cardData);
    //    NewTurn();
    //}
}
