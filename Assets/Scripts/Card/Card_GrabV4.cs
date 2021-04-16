using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR;

public class Card_GrabV4 : XRGrabInteractable
{
    public GameObject Attack_SwordPrefab;
    public GameObject Defend_SheildPrefab;
    public GameObject Spell_Damage;
    public GameObject Spell_Heal;

    private CardController _cardController;
    private CardMatController _cardMatController;

    //current hand interactor
    XRBaseInteractor currInteractor = null;
    // variable for tracking the hand interactor
    public XRNode handInteractor;
    // cache the gameobject with the hand
    GameObject handGameObject = null;


    [SerializeField] private bool isTriggerChecking = false;
    // This is the time needed before change occurs
    [SerializeField] private float TimeDuration = 1.5f;
    // This is the member variable the tracks elapsed time
    [SerializeField] private float pressedTimeDuration;
    // bool for starting timer 

    //Dictionary for gameobject prefab
    Dictionary<string, GameObject> activateDictionary = new Dictionary<string, GameObject>();

    protected override void Awake()
    {
        base.Awake();
        _cardController = GetComponent<CardController>();
        _cardMatController = GetComponent<CardMatController>();
        loadDictionary();
    }

    void loadDictionary()
    {
        activateDictionary.Add("Attack", Attack_SwordPrefab);
        activateDictionary.Add("Defend", Defend_SheildPrefab);
        activateDictionary.Add("Spell", Spell_Damage);
        activateDictionary.Add("Draw", Attack_SwordPrefab);
        activateDictionary.Add("Curse", Spell_Damage);
        activateDictionary.Add("Strength", Attack_SwordPrefab);
        activateDictionary.Add("Energy", Spell_Heal);
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
        PlayerCharacter Player = (PlayerCharacter)GameManager_BS.Instance.Player;
        var CardManaCost = _cardController.CardData.cost;

        if (Player.Mana > CardManaCost)
        {
            Player.SpendMana(CardManaCost);
            return true;
        }
        else
            return false;
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
        GameObject prefabGO = activateDictionary[cardType.ToString()];
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
