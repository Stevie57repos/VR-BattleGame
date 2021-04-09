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


    // call these methods on the enemy manager so that the UI updates
    public void TakeDamage(int damage)
    {
        Health -= damage;

    }

    // call this method on the enemy manager so that the UI updates
    public void SpendMana(int spellCost)
    {
        Mana -= spellCost;

    }



    // Start is called before the first frame update


    // Update is called once per frame
    void Update()
    {
        
    }
}
