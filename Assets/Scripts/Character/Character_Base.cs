using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Character_Base : MonoBehaviour
{
    public CharacterDataSO characterData;

    [SerializeField] public string _nameCharacter;
    [SerializeField] public int _health;
    [SerializeField] public int _maxHealth;
    [SerializeField] public int _mana;
    [SerializeField] public int _maxMana;

    public GameEvent HealthUpdate;
    public GameEvent ManaUpdate;

    protected virtual void Awake()
    {
        LoadCharacterData();
    }
    // Start is called before the first frame update
    protected virtual void Start()
    {

    }

    public abstract void TakeDamage(int damageAmount);

    public abstract void SpendMana(int spellCost);

    protected void LoadCharacterData()
    {
        this._nameCharacter = characterData.NameCharacter;
        this._health = characterData.Health;
        this._maxHealth = characterData.MaxHealth;
        this._mana = characterData.Mana;
        this._maxMana = characterData.MaxMana;
    }
}
