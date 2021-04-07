using UnityEngine;

[CreateAssetMenu(fileName = "CharacterData", menuName = "ScriptableObjects/CharacterData")]
public class CharacterDataSO : ScriptableObject
{
    public string NameCharacter;
    public int Health;
    public int MaxHealth;
    public int Mana;
    public int MaxMana;
}
