using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EnemyManager : MonoBehaviour
{
    public GameObject currentEnemyGO;
    private EnemyCharacter _currentEnemyChar;
    private EnemyStateController _currentEnemyController;
    public EnemyProjectileObjectPool EnemyProjectilePool;

    public GameEvent EnemyHealthUpdate;
    public GameEvent EnemyManaUpdate;

    public List<EnemyCharacter> enemyList = new List<EnemyCharacter>();
    public int currentLevel = 0;
    private EnemyGenerator _enemyGenerator;


    void Awake()
    {
        EnemyProjectilePool = GetComponent<EnemyProjectileObjectPool>();
        _enemyGenerator = GetComponent<EnemyGenerator>();
    }

    private void OnEnable()
    {
        GameManager_BS.Instance.battleState.OnBattleStart += BattleEnemyStart;
    }

    private void OnDisable()
    {
        GameManager_BS.Instance.battleState.OnBattleStart -= BattleEnemyStart;
    }

    public void BattleEnemyStart(GameManager_BS gameManager)
    {
        SpawnEnemy();
        UpdateEnemyUI();
    }

    private void SpawnEnemy()
    {
        if (_enemyGenerator == null) Debug.Log($" enemy generator is null");
        _enemyGenerator.SetEnemySpawnPrefab(enemyList[currentLevel].gameObject);
        currentEnemyGO = _enemyGenerator.SpawnEnemy();

        _currentEnemyController = currentEnemyGO.GetComponent<EnemyStateController>();
        _currentEnemyController.enemyManager = this;

        GameManager_BS.Instance.Enemy = _currentEnemyChar;
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
