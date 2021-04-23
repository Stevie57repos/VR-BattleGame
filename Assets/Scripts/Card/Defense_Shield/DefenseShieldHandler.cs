using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefenseShieldHandler : MonoBehaviour, ICardDataTransfer, ICardEffect
{
    [SerializeField] private int _currDurability = 2;
    [SerializeField] PlayerCharacter _playerCharacter;

    private CardScriptableObject _cardData = null;
    private CardController _cardInfo = null;

    public GameEvent ShieldDestroyed;

    [SerializeField] CharacterRegistry _characterRegistry;
    [SerializeField] CardEffectEventChannelSO _cardEffectEvent;
    [SerializeField] CardSelectionEventSO _cardSelectionEvent;


    public void TransferCardData(CardController cardInfo)
    {
        _cardInfo = cardInfo;
        _cardData = cardInfo.CardData;
    }
    private void Awake()
    {
        _playerCharacter = _characterRegistry.Player.GetComponent<PlayerCharacter>();
    }
    private void OnCollisionEnter(Collision other)
    {
        Debug.Log("collision detected");
        if (other.gameObject.GetComponent<Enemy_projectile>())
        {
            var enemyProjectile = other.gameObject.GetComponent<Enemy_projectile>();
            int chargeValue = enemyProjectile._chargeValue;

            // increase player energy by corresponding amount
            _playerCharacter.IncreaseMana(chargeValue);

            // subtract sheild durability - check durability
            CheckShieldDurability(chargeValue);

            // turn off the projectile
            enemyProjectile.gameObject.SetActive(false);
        }
        else if (other.gameObject.CompareTag("Ground"))
        {
            _cardEffectEvent.RaiseEvent(_cardInfo.gameObject, _cardData);
            ResetPlayerCardSelection();
            Destroy(this.gameObject);
        }
    }
    void CheckShieldDurability(int chargeValue)
    {
        _currDurability -= chargeValue;

        if (_currDurability <= 0)
        {
            _cardEffectEvent.RaiseEvent(_cardInfo.gameObject, _cardData);
            ResetPlayerCardSelection();
            Destroy(this.gameObject);
        }
    }

    void ResetPlayerCardSelection()
    {
        _cardSelectionEvent.RaiseEvent("None");
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

    }

    public void OnActivate()
    {

    }

    public void OnDeactivate()
    {

    }
}
