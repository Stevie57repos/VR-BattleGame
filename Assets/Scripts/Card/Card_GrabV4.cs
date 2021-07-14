using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR;

public class Card_GrabV4 : XRGrabInteractable
{
    [Header("Prefabs")]
    public GameObject Attack_SwordPrefab;
    public GameObject Defend_SheildPrefab;
    public GameObject Spell_Damage;
    public GameObject Spell_Heal;
    Dictionary<string, GameObject> ActivateDictionary = new Dictionary<string, GameObject>();

    private CardController _cardController;
    private CardMatController _cardMatController;

    [Header("Interactor")]
    private XRBaseInteractor currInteractor = null;
    public XRNode handInteractor;
    private GameObject handGameObject = null;

    [Header("Settings")]
    [SerializeField] private bool isTriggerChecking = false;
    [SerializeField] private float TimeDuration = 0.5f;
    [SerializeField] private float pressedTimeDuration;
    public CharacterRegistry _characterRegistry;

    [Header("Events")]
    [SerializeField] CardSelectionEventSO _cardSelectionEvent;
    protected override void Awake()
    {
        base.Awake();
        _cardController = GetComponent<CardController>();
        _cardMatController = GetComponent<CardMatController>();
        loadDictionary();
    }
    void loadDictionary()
    {
        ActivateDictionary.Add("Attack", Attack_SwordPrefab);
        ActivateDictionary.Add("Defend", Defend_SheildPrefab);
        ActivateDictionary.Add("Spell", Spell_Damage);
        ActivateDictionary.Add("Draw", Spell_Heal);
        ActivateDictionary.Add("Curse", Attack_SwordPrefab);
        ActivateDictionary.Add("Strength", Attack_SwordPrefab);
        ActivateDictionary.Add("Energy", Attack_SwordPrefab);
    }
    private void OnEnable()
    {
        pressedTimeDuration = TimeDuration;
    }
    protected override void OnSelectEntered(XRBaseInteractor interactor)
    {     
        base.OnSelectEntered(interactor);
        currInteractor = interactor;
        _cardMatController.SetTriggerMaterial();
        isTriggerChecking = true;
        _cardController.DeckManager.CardSelected(this.gameObject);
    }
    protected override void OnSelectExited(XRBaseInteractor interactor)
    {
        base.OnSelectExited(interactor);
        currInteractor = null;
        pressedTimeDuration = TimeDuration;
        handGameObject = null;
        isTriggerChecking = false;
        _cardController.DeckManager.CardUnselected();
    }
    void FixedUpdate()
    {
        if (isTriggerChecking)
        {
            pressedTimeDuration -= Time.fixedDeltaTime;          
            if(pressedTimeDuration <= 0)
            {
                if (CheckMana())
                {                 
                    CreateAndSelectObject();
                }
                isTriggerChecking = false;
            }
        }
        else
        {
            _cardController.SetCardMaterial();
        }    
    }
    bool CheckMana()
    {
        PlayerCharacter Player = _characterRegistry.Player.GetComponent<PlayerCharacter>();
        var CardManaCost = _cardController.CardData.cost;
        if (Player.Mana >= CardManaCost)
        {
            Player.SpendMana(CardManaCost);
            return true;
        }
        else
        {
            Debug.Log("You don't have enough mana");
            return false;
        }
    }
    private void CreateAndSelectObject()
    {
        GameObject prefabGo = CreateCardEffectObject();
        CardEffectGrab GrabComponenet = prefabGo.GetComponent<CardEffectGrab>();
        ForceSelectObject(currInteractor, GrabComponenet);
        var CardEffectController = prefabGo.GetComponent<ICardDataTransfer>();
        CardEffectController.TransferCardData(_cardController);
        gameObject.SetActive(false);
    }
    private GameObject CreateCardEffectObject()
    {
        GameObject prefabGO = Instantiate(SelectPrefab());
        return prefabGO;
    }
    private GameObject SelectPrefab()
    {
        var cardType = _cardController.CardData.type.ToString();
        GameObject prefabGO = ActivateDictionary[cardType];
        SetCardTypeSelected(cardType);
        return prefabGO;
    }
    void SetCardTypeSelected(string cardType)
    {
        _cardSelectionEvent.RaiseEvent(cardType);
    }
    private void ForceSelectObject(XRBaseInteractor Interactor ,XRGrabInteractable cardEffectGo)
    {
        interactionManager.ForceSelect(currInteractor, cardEffectGo);
    }
    private void CheckHand(XRBaseInteractor interactor)
    {
        handGameObject = interactor.gameObject;
        handInteractor = interactor.GetComponent<XRController>().controllerNode;
        isTriggerChecking = true;
    }
}
