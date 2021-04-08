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

    public event Action<int> OnDamage;

    public event Action<int> OnSpell;

    public GameObject getGameObject()
    {
        return this.gameObject;
    }

    public void TakeDamage(int damage)
    {
        Health -= damage;

        // pass the current health to the UI subscriber to update health panel
        OnDamage?.Invoke(Health);

    }

    public void SpendMana(int spellCost)
    {
        Mana -= spellCost;

        // pass the current mana to the UI subscriber to update energy panel
        OnSpell?.Invoke(Mana);
    }

    // for testing purposes
    public void Take5Damage()
    {
        TakeDamage(5);
    }

    public void Spend5Mana()
    {
        SpendMana(5);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
