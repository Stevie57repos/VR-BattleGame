using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EnemyCharacter : Character_Base, ICharacter
{
    private Animator _enemyAnimator;
    private AudioSource _audioSource;
    [SerializeField] SoundsListSO _enemyDamageSounds;

    public string NameCharacter { get { return _nameCharacter; } }

    public int Health { get { return _health; } set { _health = value; } }

    public int MaxHealth { get { return _maxHealth; } }

    public int Mana { get { return _mana; } set { _mana = value; } }

    public int MaxMana { get { return _maxMana; } }

    public GameManagerEventChannelSO WonEvent;

    protected override void Awake()
    {
        base.Awake();
        _enemyAnimator = GetComponent<Animator>();
        _audioSource = GetComponent<AudioSource>();
    }

    public GameObject getGameObject()
    {
        return this.gameObject;
    }

    public override void TakeDamage(int damageAmount)
    {
        if ((Health - damageAmount) > 0)
        {
            Health -= damageAmount;
            HealthUpdate.Raise();
            _enemyAnimator.SetTrigger("Damage_Hit");
            AudioClip randomClip = _enemyDamageSounds.SoundsArray[UnityEngine.Random.Range(0, _enemyDamageSounds.SoundsArray.Length)];
            PlayEnemyDamageSound(randomClip);
        }
        else if ((Health - damageAmount) <= 0)
        {
            Health = 0;
            HealthUpdate.Raise();
            AudioClip randomClip = _enemyDamageSounds.SoundsArray[UnityEngine.Random.Range(0, _enemyDamageSounds.SoundsArray.Length)];
            _enemyAnimator.SetBool("isDead", true);
            Debug.Log("isdead");
            //WonEvent.RaiseEvent();
            //Destroy(this.gameObject);
        }
    }

    void PlayEnemyDamageSound(AudioClip randomClip)
    {
        _audioSource.PlayOneShot(randomClip);
    }

    public override void SpendMana(int spellCost)
    {
        Mana -= spellCost;
        ManaUpdate.Raise();
    }

    public override void HealHealth(int healthAmount)
    {
        Health += healthAmount;
    }
}
