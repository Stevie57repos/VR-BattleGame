using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CardController : MonoBehaviour
{
    // scriptable object contains card data
    public CardScriptableObject cardData;

    //Card UI gameObjects
    public GameObject cardNameLabelGO;
    public GameObject cardCostLabelGO;
    public GameObject cardValueLabelGO;

    // grab the material manager
    public ICardMatGetter cardMatManager;

    private void Awake()
    {
        // grab the card material manager component
        cardMatManager = GetComponent<ICardMatGetter>();
    }

    private void Start()
    {
        // Set card material based on type. Will need to change this to card Name in the future.
        SetCardMaterial();
        // Set up Card UI to reflect card values
        SetUpCardUI();
    }

    public void SetCardMaterial()
    {
        cardMatManager.CardSetUp(cardData.name, cardData.type.ToString());
    }

    private void SetUpCardUI()
    {
        cardNameLabelGO.GetComponent<TextMeshProUGUI>().text = cardData.name;
        cardCostLabelGO.GetComponent<TextMeshProUGUI>().text = cardData.cost.ToString();
        cardValueLabelGO.GetComponent<TextMeshProUGUI>().text = cardData.value.ToString();
    }



}
