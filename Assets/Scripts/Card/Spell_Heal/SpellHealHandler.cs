using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellHealHandler : MonoBehaviour, ICardEffect, ICardDataTransfer
{
    private CardScriptableObject _cardData = null;
    private CardController _cardInfo = null;
    private PlayerCharacter _player;
    public GameEvent Event_SpellHeal;

    [SerializeField] CharacterRegistry _characterRegistry;
    [SerializeField] CardEffectEventChannelSO _cardEffectEvent;

    private void Awake()
    {
        _player = _characterRegistry.Player.GetComponent<PlayerCharacter>();
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
        PlayerCharacter playerCharacter = _characterRegistry.Player.GetComponent<PlayerCharacter>();
        playerCharacter.HealHealth(_cardData.value);
        _cardEffectEvent.RaiseEvent(_cardInfo.gameObject, _cardData);
        Destroy(this.gameObject);
    }
}
