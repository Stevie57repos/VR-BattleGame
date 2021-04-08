using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character_Base : MonoBehaviour
{
    public CharacterDataSO characterData;

    [SerializeField]protected string _nameCharacter;
    [SerializeField]protected int _health;
    [SerializeField] protected int _maxHealth;
    [SerializeField] protected int _mana;
    [SerializeField] protected int _maxMana;


    protected virtual void Awake()
    {
        LoadCharacterData();
    }
    // Start is called before the first frame update
    protected virtual void Start()
    {

    }



    // Update is called once per frame
    void Update()
    {
        
    }



    protected void LoadCharacterData()
    {
        this._nameCharacter = characterData.NameCharacter;
        this._health = characterData.Health;
        this._maxHealth = characterData.MaxHealth;
        this._mana = characterData.Mana;
        this._maxMana = characterData.MaxMana;
    }
}
