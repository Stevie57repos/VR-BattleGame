using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class CardController : MonoBehaviour
{
    // TODO : Make this private
    public DeckManager DeckManager;

    // scriptable object contains card data
    public CardScriptableObject CardData;

    //Card UI gameObjects
    public GameObject CardNameLabelGO;
    public GameObject CardCostLabelGO;
    public GameObject CardValueLabelGO;

    public CardMatController CardMatManager;

    private void Awake()
    {
        CardMatManager = GetComponent<CardMatController>();
    }

    private void Start()
    {
        SetCardMaterial();
        SetUpCardUI();
    }

    public void SetCardMaterial()
    {
        CardMatManager.CardSetUp(CardData.name, CardData.type.ToString());
    }

    private void SetUpCardUI()
    {
        CardNameLabelGO.GetComponent<TextMeshProUGUI>().text = CardData.name;
        CardCostLabelGO.GetComponent<TextMeshProUGUI>().text = CardData.cost.ToString();
        CardValueLabelGO.GetComponent<TextMeshProUGUI>().text = CardData.value.ToString();
    }



}
