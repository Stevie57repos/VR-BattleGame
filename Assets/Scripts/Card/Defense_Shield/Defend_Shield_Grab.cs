using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Defend_Shield_Grab : XRGrabInteractable
{
    [SerializeField] private int _currDurability = 2;
    [SerializeField] PlayerCharacter _playerCharacter;

    private CardScriptableObject _cardData = null;
    private CardController _cardInfo = null;

    public GameEvent ShieldDestroyed;



    protected override void Awake()
    {
        base.Awake();

    }

    private void OnCollisionEnter(Collision other)
    {
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
    }

    void CheckShieldDurability(int chargeValue)
    {
        _currDurability  -= chargeValue;

        if(_currDurability <= 0)
        {
            GameEventsHub.ShieldDestroyed.CardGO = _cardInfo.gameObject;
            GameEventsHub.ShieldDestroyed.CardSO = _cardData;
            ShieldDestroyed.Raise();
            Destroy(this);
        }
    }
}
