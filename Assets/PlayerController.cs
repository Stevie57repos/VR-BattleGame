using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CardTypeSelected { None, Attack, SpellDamage, Defend, SpellHeal }
public class PlayerController : MonoBehaviour
{
    public CardTypeSelected CardType;
    private Dictionary<string, CardTypeSelected> CardTypeDictionary = new Dictionary<string, CardTypeSelected>();
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
    }

    private void LoadDictionary()
    {
        CardTypeDictionary.Add("None", CardTypeSelected.None);
        CardTypeDictionary.Add("Attack", CardTypeSelected.Attack);
        CardTypeDictionary.Add("Defend", CardTypeSelected.Defend);
        CardTypeDictionary.Add("Spell", CardTypeSelected.SpellDamage);
        CardTypeDictionary.Add("Draw", CardTypeSelected.SpellHeal);
    }
    public void EventCardSelected()
    {
        CardType = CardTypeDictionary[GameEventsHub.CardSelected.CardTypeString];
    }

    public void SetCardTypeSelection(string cardType)
    {
        CardType = CardTypeDictionary[cardType];
    }

}
