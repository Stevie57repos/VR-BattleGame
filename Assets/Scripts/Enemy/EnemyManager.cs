using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EnemyManager : MonoBehaviour
{
    public GameManager_BS gameManager;
    public GameObject currentEnemyGO;
    private EnemyCharacter _currentEnemyChar;
    private EnemyStateController _currentEnemyController;
    public EnemyProjectileObjectPool EnemyProjectilePool;

    public GameEvent EnemyHealthUpdate;
    public GameEvent EnemyManaUpdate;

    public List<EnemyCharacter> enemyList = new List<EnemyCharacter>();
    public int currentLevel = 0;
    EnemyGenerator enemyGenerator;


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
        SpawnEnemy();
        UpdateEnemyUI();
    }

    private void SpawnEnemy()
    {
        enemyGenerator = GetComponent<EnemyGenerator>();
        enemyGenerator.SetEnemySpawnPrefab(enemyList[currentLevel].gameObject);
        currentEnemyGO = enemyGenerator.SpawnEnemy();

        _currentEnemyController = currentEnemyGO.GetComponent<EnemyStateController>();
        _currentEnemyController.enemyManager = this;

        gameManager.Enemy = _currentEnemyChar;
    }

    void UpdateEnemyUI()
    {
        _currentEnemyChar = currentEnemyGO.GetComponent<EnemyCharacter>();
        _currentEnemyChar.HealthUpdate = EnemyHealthUpdate;
        _currentEnemyChar.ManaUpdate = EnemyManaUpdate;

        _currentEnemyChar.TakeDamage(0);
        _currentEnemyChar.SpendMana(0);
    }

    public void EnemyTake5Damage()
    {
        _currentEnemyChar.TakeDamage(5);
    }

    public void EnemySpend5Mana()
    {
        _currentEnemyChar.SpendMana(5);
    }

}
