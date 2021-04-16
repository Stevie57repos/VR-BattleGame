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

    protected override void Start()
    {
        base.Start();
        RegisterPlayer();
    }

    private void RegisterPlayer()
    {
        GameManager_BS.Instance.Player = this;
    }

    public GameObject getGameObject()
    {
        return this.gameObject;
    }

    public override void TakeDamage(int damageAmount)
    {
        Health -= damageAmount;
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
}
