using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EnemyManager : MonoBehaviour
{
    public GameManager_BS gameManager;
    public EnemyCharacter currentEnemy;
    public EnemyProjectileObjectPool EnemyProjectilePool;

    public GameEvent EnemyHealthUpdate;
    public GameEvent EnemyManaUpdate;

    public List<EnemyCharacter> enemyList = new List<EnemyCharacter>();
    public int currentLevel = 0;
    IEnemyGetter enemyGenerator;


    void awake()
    {
        if(gameManager == null)
            gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager_BS>();
        EnemyProjectilePool = GetComponent<EnemyProjectileObjectPool>();

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
        enemyGenerator = GetComponent<EnemyGenerator>();

        enemyGenerator.SetEnemySpawnPrefab(enemyList[currentLevel].gameObject);

        EnemyCharacter enemy = enemyGenerator.SpawnEnemy();
        enemy.HealthUpdate = EnemyHealthUpdate;
        enemy.ManaUpdate = EnemyManaUpdate;

        // register enemy
        gameManager.Enemy = enemy;
        currentEnemy = enemy;

        // Update the enemy stats panels
        enemy.TakeDamage(0);
        enemy.SpendMana(0);
    }

    public void EnemyTake5Damage()
    {
        currentEnemy.TakeDamage(5);
    }

    public void EnemySpend5Mana()
    {
        currentEnemy.SpendMana(5);
    }

}
