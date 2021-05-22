using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class CardController : MonoBehaviour
{
    public DeckManager DeckManager;
    // scriptable object contains card data
    public CardScriptableObject CardData;

    //Card UI gameObjects
    public GameObject CardNameLabelGO;
    public GameObject CardCostLabelGO;
    public GameObject CardValueLabelGO;

    public CardMatController CardMatManager;
    [SerializeField] RewardCardHandler _rewardCardHandler;
    [SerializeField] CharacterRegistry _characterRegistry;

    private void Awake()
    {
        if (CardMatManager == null)
            CardMatManager = GetComponent<CardMatController>();
    }

    public void SetupCard(CardScriptableObject cardData)
    {
        CardData = cardData;
        SetCardMaterial();
        SetUpCardUI();
    }

    public void SetupReward(RewardsManager rewardsManager, CardScriptableObject cardData)
    {
        if (_rewardCardHandler == null)
            _rewardCardHandler = GetComponent<RewardCardHandler>();
        _rewardCardHandler.SetRewardsManager(rewardsManager);
        SetupCard(cardData);
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
