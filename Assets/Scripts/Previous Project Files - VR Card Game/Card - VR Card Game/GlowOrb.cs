using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class GlowOrb : XRGrabInteractable, ICardObjectGetter
{

    // tracking if its in the air
    private bool inAir = false;

    // card object variable
    public CardGameObject cardObject;

    //list of cardeffectPrefab
    public List<GameObject> cardEffectPrefabsList = new List<GameObject>();
    // dictionary list for which prefab to create
    Dictionary<string, GameObject> cardEffectPrefabs = new Dictionary<string, GameObject>();

    // Set initial duration time
    [SerializeField] private float TimeDuration = 1.5f;
    // setting timer to 1 second before changing
    [SerializeField] private float pressedTimeDuration;


    private void Start()
    {
        Debug.Log($"This is gloworb for {cardObject.cardData.name} and the value is {cardObject.cardData.value}");
        Debug.Log($"the gloworb cardType is {cardObject.cardData.type}");

        // Set up dictionary
        loadDictionary();
    }

    void loadDictionary()
    {
        cardEffectPrefabs.Add("Attack", cardEffectPrefabsList[0]);
        cardEffectPrefabs.Add("Defend", cardEffectPrefabsList[1]);
        cardEffectPrefabs.Add("Spell", cardEffectPrefabsList[2]);
        cardEffectPrefabs.Add("Draw", cardEffectPrefabsList[3]);
        cardEffectPrefabs.Add("Curse", cardEffectPrefabsList[4]);
        cardEffectPrefabs.Add("Strength", cardEffectPrefabsList[5]);
        cardEffectPrefabs.Add("Energy", cardEffectPrefabsList[6]);
    }

    // On Grab
    protected override void OnSelectEntered(XRBaseInteractor interactor)
    {
        base.OnSelectEntered(interactor);
        inAir = false;
    }

    // On Release
    protected override void OnSelectExited(XRBaseInteractor interactor)
    {
        base.OnSelectExited(interactor);
        inAir = true;
    }


    private void FixedUpdate()
    {
        if (inAir)
        {
            // Start the countdown
            TimeDuration -= Time.fixedDeltaTime;
        }

        if (TimeDuration <= 0f)
        {
            CreateCardEffect();

            // turn off the tracker
            inAir = false;

            // turn off this object
            this.gameObject.SetActive(false);
        }
    }


    void CreateCardEffect()
    {
        // saving the location of where this object is
        Vector3 spawnPos = new Vector3(transform.position.x, transform.position.y, transform.position.z);

        // instantiating the prefab at this objects current location **
        GameObject cardEffectObject = Instantiate(cardEffectPrefabs[cardObject.cardData.type.ToString()], spawnPos, Quaternion.identity);

        // Utilizing an interface to pass the cardObject variable reference 
        ICardObjectGetter cardEffectComponent = cardEffectObject.GetComponent<ICardObjectGetter>();

        // passing the cardObject reference to the componenent
        cardEffectComponent.GetCardObject(cardObject);

    }

    public void GetCardObject(CardGameObject cardObjectData)
    {
        cardObject = cardObjectData;
    }
}
