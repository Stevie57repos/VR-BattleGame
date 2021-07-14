using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Registries/Character List")]
public class CharacterRegistry : ScriptableObject
{
    public GameObject Player;
    public GameObject CurrentEnemy;
}
