using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EnemyCharacter : Character_Base, ICharacter
{
    public string NameCharacter { get { return _nameCharacter; } }

    public int Health { get { return _health; } set { _health = value; } }

    public int MaxHealth { get { return _maxHealth; } }

    public int Mana { get { return _mana; } set { _mana = value; } }

    public int MaxMana { get { return _maxMana; } }

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



    // Start is called before the first frame update


    // Update is called once per frame
    void Update()
    {
        
    }
}
