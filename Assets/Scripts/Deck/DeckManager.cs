using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeckManager : MonoBehaviour
{
    public GameObject CardspawnSpotGO;
    private GameManager_BS _gameManager;
    private int _maxHandSize = 5;

    [SerializeField] private GameObject _cardPrefab;
    [SerializeField] private DeckScriptableObject _deckData;

    [Header("Game Manager Events")]
    [SerializeField] GameManagerEventChannelSO _gameManagerLossEvent;
    [SerializeField] GameManagerEventChannelSO _gameManagerBattleFinishEvent;
    [SerializeField] GameManagerEventChannelSO _gameManagerBattleStart;
    [SerializeField] CardEffectEventChannelSO _cardEffectEvent;
    [SerializeField] CharacterRegistry _characterRegistry;
    private PlayerController _playerController;

    [Header("Card List")]
    public List<CardScriptableObject> DeckSO;
	public List<CardScriptableObject> HandSO;
	public List<CardScriptableObject> GraveyardSO;
	public List<CardScriptableObject> BurnSO;
    public List<GameObject> HandCards;
    private float cardSpreadDistance = 0f;

    private bool isInitialDeckLoaded = false;
    private void OnEnable()
    {
        _gameManagerBattleStart.GameManagerEvent += DeckStart;
        _cardEffectEvent.OnCardEffectActivate += NewTurnV2;
        _gameManagerBattleFinishEvent.GameManagerEvent += BattleFinish;
        _gameManagerLossEvent.GameManagerEvent += BattleFinish;
    }
    private void OnDisable()
    {
        _gameManagerBattleStart.GameManagerEvent -= DeckStart;
        _cardEffectEvent.OnCardEffectActivate -= NewTurnV2;
        _gameManagerBattleFinishEvent.GameManagerEvent -= BattleFinish;
        _gameManagerLossEvent.GameManagerEvent -= BattleFinish;
    }
    private void Awake()
    {
        _gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager_BS>();
    }
    void DeckStart()
    {
        if(!isInitialDeckLoaded)
            LoadCards();
        ShuffleDeck();
        Draw(3);
    }
	void LoadCards()
    {
        isInitialDeckLoaded = true;
		foreach (CardScriptableObject card in _deckData.cards)
        {
			this.DeckSO.Add(card);
        }
    }
    public void ShuffleDeck()
    {
		for(int t = 0; t < DeckSO.Count; t++)
        {
            CardScriptableObject tmp = DeckSO[t];
            int r = Random.Range(t, DeckSO.Count);
            DeckSO[t] = DeckSO[r];
            DeckSO[r] = tmp;
        }
    }
    public CardScriptableObject GetNextCard()
    {
        return DeckSO[0];
    }
    public void Draw(int n =1)
    {
        for(int i = 0; i < n; i++)
        {
            // check if DeckSO is empty
            if(DeckSO.Count == 0)
            {
                RefillDeck();
            }
            else
            {
                CardScriptableObject card = GetNextCard();
                if (HandSO.Count > _maxHandSize)
                {
                    Overdraw(card);
                }
                else
                {
                    SpawnCard(card);
                    HandSO.Add(card);
                    DeckSO.Remove(card);
                }
            }
        }
        FanCards();
    }
    private void FanCards()
    {
        for (int i = 0; i < HandCards.Count; i++)
        {
            var CardspawnPos = CardspawnSpotGO.transform.position;
            Vector3 cardSpot = new Vector3((cardSpreadDistance + CardspawnPos.x), CardspawnPos.y, CardspawnPos.z);
            HandCards[i].transform.position = cardSpot;
            cardSpreadDistance += 0.25f;
        }
        cardSpreadDistance = 0f;
    }
    public void CardSelected(GameObject SelectedCard)
    {
        foreach (GameObject card in HandCards)
        {
            if(SelectedCard.gameObject != card)
            {
                card.SetActive(false);
            }
        }
    }
    public void CardUnselected()
    {
        foreach (GameObject card in HandCards)
        {
            card.SetActive(true);
        }
        FanCards();
    }
    private void SpawnCard(CardScriptableObject card)
    {
        GameObject cardObject = Instantiate(_cardPrefab);
        var cardController = cardObject.GetComponent<CardController>();
        cardController.SetupCard(card); 
        cardController.CardData = card;
        cardController.DeckManager = this;
        HandCards.Add(cardObject);
    }
    private void RefillDeck()
    {
        foreach(CardScriptableObject card in GraveyardSO)
        {
            DeckSO.Add(card);
        }
        GraveyardSO.Clear();
        ShuffleDeck();
        Draw();
    }
    private void Overdraw(CardScriptableObject card)
    {
        Debug.Log("Overdrew a " + card.name + " card! It was sent to the GraveyardSO!");
        GraveyardSO.Add(card);
        DeckSO.Remove(card);
    }
    public void BurnCard(CardScriptableObject card)
    {
        Debug.Log("Burned a " + card.name + " card! It was sent to the burned pile It will not be refilled!");
        BurnSO.Add(card);
        HandSO.Remove(card);
    }
    private void NewTurnV2(GameObject cardObject, CardScriptableObject cardData)
    {
        UpdateCardLists(cardObject, cardData);
        if (_playerController == null)
            _playerController = _characterRegistry.Player.GetComponent<PlayerController>();
        _playerController.CurrentStatus = PlayerStatus.isIdle;
        _playerController.CardType = CardTypeSelected.None;

        if (_gameManager.CheckIfInBattleState())
        {
            CardUnselected();
            Draw();
        }
    }
    public void UpdateCardLists(GameObject cardGO, CardScriptableObject card)
    {
        GraveyardSO.Add(card);
        HandSO.Remove(card);
        HandCards.Remove(cardGO);
        Destroy(cardGO);
    }
    private void BattleFinish()
    {
        SendHandToGraveyard();
        foreach (CardScriptableObject card in GraveyardSO)
        {
            DeckSO.Add(card);
        }
        GraveyardSO.Clear();
    }
    private void SendHandToGraveyard()
    {
        for (int i = HandCards.Count; i > 0; i--)
        {
            UpdateCardLists(HandCards[i - 1], HandCards[i - 1].GetComponent<CardController>().CardData);
        }
    }
    public void AddToDeck(CardScriptableObject card)
    {
        DeckSO.Add(card);
    }
}
