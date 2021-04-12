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


}
