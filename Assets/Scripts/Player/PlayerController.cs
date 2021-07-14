using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum PlayerStatus { isIdle, isInProgress }
public enum CardTypeSelected { None, Attack, SpellDamage, Defend, SpellHeal }
public class PlayerController : MonoBehaviour
{
    public PlayerStatus CurrentStatus;
    public CardTypeSelected CardType;
    private Dictionary<string, CardTypeSelected> _cardTypeDictionary = new Dictionary<string, CardTypeSelected>();
    [SerializeField] CardSelectionEventSO _cardSelectionEvent;
    private void Awake()
    {
        LoadDictionary();
    }
    private void OnEnable()
    {
        _cardSelectionEvent.OnCardSelection += SetCardTypeSelection;
    }
    private void OnDisable()
    {
        _cardSelectionEvent.OnCardSelection -= SetCardTypeSelection;
    }
    private void Start()
    {
        CardType = CardTypeSelected.None;
        CurrentStatus = PlayerStatus.isIdle;
    }
    private void LoadDictionary()
    {
        _cardTypeDictionary.Add("None", CardTypeSelected.None);
        _cardTypeDictionary.Add("Attack", CardTypeSelected.Attack);
        _cardTypeDictionary.Add("Defend", CardTypeSelected.Defend);
        _cardTypeDictionary.Add("Spell", CardTypeSelected.SpellDamage);
        _cardTypeDictionary.Add("Draw", CardTypeSelected.SpellHeal);
    }
    public void EventCardSelected()
    {
        CardType = _cardTypeDictionary[GameEventsHub.CardSelected.CardTypeString];
    }
    public void SetCardTypeSelection(string cardType)
    {
        CardType = _cardTypeDictionary[cardType];
    }
}
