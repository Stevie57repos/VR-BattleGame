using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellHealHandler : MonoBehaviour, ICardEffect, ICardDataTransfer
{
    private CardScriptableObject _cardData = null;
    private CardController _cardInfo = null;
    private PlayerCharacter _player;
    public GameEvent Event_SpellHeal;

    private void Awake()
    {
        _player = (PlayerCharacter) GameManager_BS.Instance.Player;
    }

    public void TransferCardData(CardController cardInfo)
    {
        _cardInfo = cardInfo;
        _cardData = cardInfo.CardData;
    }


    public void OnActivate()
    {

    }

    public void OnDeactivate()
    {

    }

    public void OnHoverEntered()
    {

    }

    public void OnHoverExited()
    {

    }

    public void OnSelectEntered()
    { 

    }

    public void OnSelectExited()
    {
        SpellHealEvent();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SpellHealEvent()
    {
        _player.HealHealth(_cardData.value);
        GameEventsHub.SpellHeal.CardGO = _cardInfo.gameObject;
        GameEventsHub.SpellHeal.CardSO = _cardData;
        Event_SpellHeal.Raise();
        Destroy(this.gameObject);
    }
}
