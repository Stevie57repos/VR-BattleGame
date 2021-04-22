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

    private CardController _cardController;
    private CardMatController _cardMatController;

    public GameEvent CardSelected;

    //current hand interactor
    XRBaseInteractor currInteractor = null;
    // variable for tracking the hand interactor
    public XRNode handInteractor;
    // cache the gameobject with the hand
    GameObject handGameObject = null;

    public CharacterRegistry _characterRegistry;

    [SerializeField] private bool isTriggerChecking = false;
    // This is the time needed before change occurs
    [SerializeField] private float TimeDuration = 1.5f;
    // This is the member variable the tracks elapsed time
    [SerializeField] private float pressedTimeDuration;
    // bool for starting timer 

    //Dictionary for gameobject prefab
    Dictionary<string, GameObject> ActivateDictionary = new Dictionary<string, GameObject>();

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

    // on interactable object pickup
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

        if (Player.Mana > CardManaCost)
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
        // Create Card Effect Object
        GameObject prefabGo = CreateCardEffectObject();

        //handSword handSwordGO = CreateCardEffect();
        CardEffectGrab GrabComponenet = prefabGo.GetComponent<CardEffectGrab>();
        ForceSelectObject(currInteractor, GrabComponenet);

        var CardEffectController = prefabGo.GetComponent<ICardDataTransfer>();
        CardEffectController.TransferCardData(_cardController);

        GameEventsHub.CardSelected.CardTypeString = _cardController.CardData.type.ToString();
        CardSelected?.Raise();

        gameObject.SetActive(false);
    }

    private GameObject CreateCardEffectObject()
    {
        GameObject prefabGO = Instantiate(SelectPrefab());
        return prefabGO;
    }

    //private handSword CreateCardEffect()
    //{
    //    GameObject prefabGO = Instantiate(SelectPrefab());
    //    // TO DO : Change this later to proper interface
    //    handSword cardEffect = prefabGO.GetComponent<handSword>();
    //    return cardEffect;
    //}



    private GameObject SelectPrefab()
    {
        var cardType = _cardController.CardData.type;
        GameObject prefabGO = ActivateDictionary[cardType.ToString()];
        return prefabGO;
    }


    private void ForceSelectObject(XRBaseInteractor interactor ,XRGrabInteractable cardEffectGo)
    {
        interactionManager.ForceSelect(currInteractor, cardEffectGo);
    }

    private void CheckHand(XRBaseInteractor interactor)
    {
        // need to use interactor to determine which hand controller we need to check
        Debug.Log($"the interactor is {interactor.gameObject.name}");

        handGameObject = interactor.gameObject;

        //identify which hand interactor is grabbing this object
        handInteractor = interactor.GetComponent<XRController>().controllerNode;
        Debug.Log($"the interaactor is {handInteractor.ToString()} ");

        //set is triggerChecking to true so that update starts tracking the controller
        isTriggerChecking = true;
    }
}
