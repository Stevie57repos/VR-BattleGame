using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;   

public class PlayerCharacter : Character_Base, ICharacter
{ 
    #region Player Stat Variables 
    public string NameCharacter { get { return _nameCharacter ; } }
    public int Health { get { return _health ; } set { _health = value; } }
    public int MaxHealth { get { return _maxHealth; } }
    public int Mana { get { return _mana; } set { _mana = value; } }
    public int MaxMana { get { return _maxMana; } }
    #endregion

    //[SerializeField] GameManagerEventChannelSO BattleStartEvent;
    [SerializeField] GameManagerEventChannelSO LossEvent;

    protected override void Start()
    {
        base.Start();
    }

    void OnEnable()
    {
        //BattleStartEvent.GameManagerEvent += BattleStartUISetup;
    }

    void OnDisable()
    {
        //BattleStartEvent.GameManagerEvent -= BattleStartUISetup;
    }

    public void BattleStartUISetup()
    {
        TakeDamage(0);
        SpendMana(0);
    }

    public GameObject getGameObject()
    {
        return this.gameObject;
    }

    public override void TakeDamage(int damageAmount)
    {
        if ((Health - damageAmount) > 0)
        {
            Health -= damageAmount;
        }
        else if((Health - damageAmount) <= 0)
        {
            Health = 0;
            LossEvent.RaiseEvent();
            HealthUpdate.Raise();
            this._health = characterData.Health;
        }
        HealthUpdate.Raise();
    }

    public override void SpendMana(int spellCost)
    {
        Mana -= spellCost;
        ManaUpdate.Raise();
    }

    public void IncreaseMana(int chargeValue)
    {
        Mana += chargeValue;
        ManaUpdate.Raise();
    }

    // These methods are for testing purposes
    public void Take5Damage()
    {
        TakeDamage(5);
    }

    public void Spend5Mana()
    {
        SpendMana(5);
    }

    // TODO : Double check that heal health doesn't exceed max health;
    public override void HealHealth(int healthAmount)
    {
        if(healthAmount == 0)
            Health += GameEventsHub.SpellHeal.CardSO.value;
        else
        {
            Health += healthAmount;
        }
        HealthUpdate.Raise();
    }
}
