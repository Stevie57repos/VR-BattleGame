using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Character_Base : MonoBehaviour
{
    public CharacterDataSO CharacterData;

    [Header("Player Stats")]
    [SerializeField] public string _nameCharacter;
    [SerializeField] public int _health;
    [SerializeField] public int _maxHealth;
    [SerializeField] public int _mana;
    [SerializeField] public int _maxMana;

    [Header("Events")]
    public GameEvent HealthUpdate;
    public GameEvent ManaUpdate;
    protected virtual void Awake()
    {
        LoadCharacterData();
    }
    protected virtual void Start()
    {
    }
    public abstract void TakeDamage(int damageAmount);
    public abstract void SpendMana(int spellCost);
    public abstract void HealHealth(int healthAmount);
    protected void LoadCharacterData()
    {
        this._nameCharacter = CharacterData.NameCharacter;
        this._health = CharacterData.Health;
        this._maxHealth = CharacterData.MaxHealth;
        this._mana = CharacterData.Mana;
        this._maxMana = CharacterData.MaxMana;
    }
}
