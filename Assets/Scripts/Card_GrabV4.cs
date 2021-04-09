using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR;

public class Card_GrabV4 : XRGrabInteractable
{
    #region Gameobjects and Componenets variables

    // prefab variable reference for glow Orbs
    public GameObject HandSwordPrefab;

    // access to CardGameObject
    CardController _cardController;

    // this shouldn't be needed
    // access to game maanger
    GameManager_BS gameManager;

    // access to CardMatController
    CardMatController _cardMatController;

    #endregion

    #region Hand Interactor Variables

    //current hand interactor
    XRBaseInteractor currInteractor = null;

    // variable for tracking the hand interactor
    public XRNode handInteractor;

    // cache the gameobject with the hand
    GameObject handGameObject = null;


    [SerializeField] private bool isTriggerChecking = false;
    // This is the time needed before change occurs
    [SerializeField] private float TimeDuration = 3f;
    // This is the member variable the tracks elapsed time
    [SerializeField] private float pressedTimeDuration;
    // bool for starting timer 
    //[SerializeField] private bool isTrackingTime = false;

    #endregion

    //Dictionary for gameobject prefab
    Dictionary<string, GameObject> activateDictionary = new Dictionary<string, GameObject>();

    protected override void Awake()
    {
        base.Awake();
        // grab cardObject component
        _cardController = GetComponent<CardController>();
        _cardMatController = GetComponent<CardMatController>();

        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager_BS>();


        // initiality dictionary. Dictionary should be moved to GameManager so it is only run once and used once.
        loadDictionary();
    }

    // Use different glow orbs in the future depending on cardtype
    void loadDictionary()
    {
        activateDictionary.Add("Attack", HandSwordPrefab);
        activateDictionary.Add("Defend", HandSwordPrefab);
        activateDictionary.Add("Spell", HandSwordPrefab);
        activateDictionary.Add("Draw", HandSwordPrefab);
        activateDictionary.Add("Curse", HandSwordPrefab);
        activateDictionary.Add("Strength", HandSwordPrefab);
        activateDictionary.Add("Energy", HandSwordPrefab);
    }

    private void OnEnable()
    {
        // Setting the time tracking variable to the Time duration
        pressedTimeDuration = TimeDuration;
    }

    protected override void OnSelectEntered(XRBaseInteractor interactor)
    {
       // there is an error with cardOwner. It is currently null
       // if (CardObject.cardOwner.ToString() == gameManager.GetPlayerName(gameManager.activePlayer))
        
        base.OnSelectEntered(interactor);
        // identify which hand interactor and start checking for trigger press on that interactor
        currInteractor = interactor;
        Debug.Log("card has been picked up");

        // card has been picked up. Trnasform it
        _cardMatController.SetTriggerMaterial();

        isTriggerChecking = true;
       
    }


    protected override void OnSelectExited(XRBaseInteractor interactor)
    {
        base.OnSelectExited(interactor);
        //reset saved hand interactor upon letting go of the object
        currInteractor = null;
        pressedTimeDuration = TimeDuration;
        handGameObject = null;
        isTriggerChecking = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // checking if the trigger button is pressed
        if (isTriggerChecking)
        {
            // timer decreases
            pressedTimeDuration -= Time.fixedDeltaTime;
                
            if(pressedTimeDuration <= 0)
            {
                // spawn the glowing orb
                Debug.Log($"Spawn Glowing Orb");


                if (CheckMana())
                {                 
                    CreateAndSelectObject();
                }

                // turn off time tracking
                isTriggerChecking = false;
            }
        }
        else
        {
            //apply default mataerial
            _cardController.SetCardMaterial();
        }

      
    }

    bool CheckMana()
    {
        var Player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerCharacter>();
        var CardManaCost = _cardController.cardData.cost;

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
        Debug.Log("object created");
        handSword handSwordGO = CreateCardEffect();

        // turn off the cardobject
        gameObject.SetActive(false);

        // force the hand to select the glowing orb
        ForceSelectObject(currInteractor, handSwordGO);
    }

    
    private handSword CreateCardEffect()
    {
        GameObject prefabGO = Instantiate(HandSwordPrefab);
        // TO DO : Change this later to proper interface
        handSword cardEffect = prefabGO.GetComponent<handSword>();
        return cardEffect;
    }


    private void ForceSelectObject(XRBaseInteractor interactor ,XRGrabInteractable cardEffectGo)
    {
        interactionManager.ForceSelect(currInteractor, cardEffectGo);
    }

    //private void CreateAndSelectOrb()
    //{
    //    GlowOrb gloworb = CreateOrb(SelectCardType());

    //    // turn off the cardobject
    //    gameObject.SetActive(false);

    //    // force the hand to select the glowing orb
    //    SelectOrb(gloworb);
    //}

    //private GameObject SelectCardType()
    //{
    //    // convert the card type into a string
    //    string cardTypeName = CardObject.cardData.type.ToString();

    //    // search the dictionary for the card type and return the associated value
    //    return activateDictionary[cardTypeName];

    //}

    //private GlowOrb CreateOrb(GameObject prefab)
    //{      
    //    GameObject glowOrbObject = Instantiate(prefab);

    //    // Pass on variable values from CardGameObject into GlowOrb
    //    ICardObjectGetter glowOrbCardData = glowOrbObject.GetComponent<ICardObjectGetter>();
    //    glowOrbCardData.GetCardObject(CardObject);

    //    return glowOrbObject.GetComponent<GlowOrb>();
    //}

    //private void SelectOrb(GlowOrb gloworb)
    //{
    //    interactionManager.ForceSelect(currInteractor, gloworb);
    //}



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
