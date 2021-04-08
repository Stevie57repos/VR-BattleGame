using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour, IEnemyManager
{
    public GameManager_BS gameManager;

    public List<EnemyCharacter> enemyList = new List<EnemyCharacter>();

    public int Level = 0;

    IEnemyGetter enemyGenerator;

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

        enemyGenerator.SpawnEnemy();

        gameManager.Enemy = GameObject.FindGameObjectWithTag("Enemy").GetComponent<ICharacter>();

  
    }

}
