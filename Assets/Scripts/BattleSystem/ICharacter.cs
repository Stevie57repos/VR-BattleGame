using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICharacter 
{
    string NameCharacter { get; }
    int Health { get; set; }
    int MaxHealth { get; }
    int Mana { get; set; }
    int MaxMana { get; }

    GameObject getGameObject();

    void TakeDamage(int damage);

    void SpendMana(int Mana);
}
