using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR;

public class Card_GrabV3 : XRGrabInteractable
{
    #region Gameobjects and Componenets variables

    //prefab variable reference for glow Orbs
    public GameObject GlowOrbPrefab;

    //access to CardGameObject
    CardGameObject CardObject;

    GameManagerObject gameManager;

    #endregion

    #region Renderer materials variables
    // Variables for material change when the trigger button is activated
    private Renderer meshRend;
    [SerializeField] private Material Mat_Default;
    [SerializeField] private Material Mat_Electrical;
    public Material testMaterial;
    #endregion

    #region Hand Inteeractor Variables

    //current hand interactor
    XRBaseInteractor currInteractor = null;
    //bool for tracking device trigger press
    bool isTriggerChecking = false;
    // variable for tracking the hand interactor
    public XRNode handInteractor;
    // cache the gameobject with the hand
    GameObject handGameObject = null;
    // This is the time needed before change occurs
    [SerializeField] private float TimeDuration = 3f;
    // This is the member variable the tracks elapsed time
    [SerializeField] private float pressedTimeDuration;
    // bool for starting timer 
    [SerializeField] private bool isTrackingTime = false;

    #endregion

    //Dictionary for gameobject prefab
    Dictionary<string, GameObject> activateDictionary = new Dictionary<string, GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManagerObject>();

        // grab cardObject component
        CardObject = GetComponent<CardGameObject>();

        // get mesh renderer compononent
        //meshRend = GetComponent<MeshRenderer>();
        // save default material
        //Mat_Default = meshRend.material;

        // initiality dictionary. Dictionary should be moved to GameManager so it is only run once and used once.
        loadDictionary();

  }


    // Use different glow orbs in the future depending on cardtype
    void loadDictionary()
    {
        activateDictionary.Add("Attack", GlowOrbPrefab);
        activateDictionary.Add("Defend", GlowOrbPrefab);
        activateDictionary.Add("Spell", GlowOrbPrefab);
        activateDictionary.Add("Draw", GlowOrbPrefab);
        activateDictionary.Add("Curse", GlowOrbPrefab);
        activateDictionary.Add("Strength", GlowOrbPrefab);
        activateDictionary.Add("Energy", GlowOrbPrefab);
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
        
    }


    protected override void OnSelectExited(XRBaseInteractor interactor)
    {
        base.OnSelectExited(interactor);
        //reset saved hand interactor upon letting go of the object
        currInteractor = null;
        isTriggerChecking = false;
        pressedTimeDuration = TimeDuration;
        handGameObject = null;
    }

    protected override void OnActivate(XRBaseInteractor interactor)
    {
        isTriggerChecking = true;
    }

    protected override void OnDeactivate(XRBaseInteractor interactor)
    {
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


                if (CardObject.SpendEnergy())
                {
                    CreateAndSelectOrb();
                }
                // turn off time tracking
                isTriggerChecking = false;
            }
        }
        else
        {
            //apply default mataerial
            SetDefaultMat();
        }     
    }

    private void CreateAndSelectOrb()
    {
        // create IGlowOrb Interface and set it to that
        // spawn the GlowOrb based on the card type and immedieatly return the GlowOrb componenet
        GlowOrb gloworb = CreateOrb(SelectCardType());

        // turn off the cardobject
        gameObject.SetActive(false);

        // force the hand to select the glowing orb
        SelectOrb(gloworb);
    }

    private GameObject SelectCardType()
    {
        // convert the card type into a string
        string cardTypeName = CardObject.cardData.type.ToString();

        // search the dictionary for the card type and return the associated value
        return activateDictionary[cardTypeName];
      
    }

    private GlowOrb CreateOrb(GameObject prefab)
    {      
        GameObject glowOrbObject = Instantiate(prefab);
        
        // Pass on variable values from CardGameObject into GlowOrb
        ICardObjectGetter glowOrbCardData = glowOrbObject.GetComponent<ICardObjectGetter>();
        glowOrbCardData.GetCardObject(CardObject);

        return glowOrbObject.GetComponent<GlowOrb>();
    }

    private void SelectOrb(GlowOrb gloworb)
    {
        interactionManager.ForceSelect(currInteractor, gloworb);
    }

    private void triggerElectricalMat()
    {
        Material[] eletricalMatArray = { Mat_Default, Mat_Electrical};
        meshRend.materials = eletricalMatArray;
    }

    private void SetDefaultMat()
    {
        CardObject.SetCardMaterial();
    }


    private bool checkTrigger()
    {
        //check the trigger button on the hand interactor that grabbed the card
        InputDevice device = InputDevices.GetDeviceAtXRNode(handInteractor);
        device.TryGetFeatureValue(CommonUsages.triggerButton, out bool isPressed);      
        Debug.Log($"The device at {handInteractor} is currrently {isPressed}");
        return isPressed;
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
