using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EnemyManager : MonoBehaviour, IEnemyManager
{
    public GameManager_BS gameManager;

    public List<EnemyCharacter> enemyList = new List<EnemyCharacter>();

    public int Level = 0;

    IEnemyGetter enemyGenerator;

    public event Action<int> OnDamage;

    public event Action<int> OnSpell;

    void awake()
    {
        if(gameManager == null)
            gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager_BS>();
    }

    private void OnEnable()
    {
        gameManager.battleState.OnBattleStart += BattleEnemyStart;
    }

    private void OnDisable()
    {
        gameManager.battleState.OnBattleStart -= BattleEnemyStart;
    }

    public void BattleEnemyStart(GameManager_BS gameManager)
    {
        // get component
        enemyGenerator = GetComponent<EnemyGenerator>();
        // Enemy list position represents the level opponent
        enemyGenerator.SetEnemySpawnPrefab(enemyList[Level].gameObject);

        EnemyCharacter enemy = enemyGenerator.SpawnEnemy();

        UI_Manager UIManager = (UI_Manager)gameManager.UImanager;

        takeDamage(enemy, 0);
        spendMana(enemy, 0);
        gameManager.Enemy = enemy;
    }

    // take damage is on the enemy manager
    public void takeDamage(EnemyCharacter enemy,int damage)
    {
        enemy.TakeDamage(damage);
        // pass the current health to the UI subscriber to update health panel
        OnDamage?.Invoke(enemy.Health);
    }

    // spend mana is on the enemy manager
    public void spendMana(EnemyCharacter enemy, int manaCost)
    {
        enemy.SpendMana(manaCost);
        // pass the current mana to the UI subscriber to update energy panel
        OnSpell?.Invoke(enemy.Mana);
    }

}
