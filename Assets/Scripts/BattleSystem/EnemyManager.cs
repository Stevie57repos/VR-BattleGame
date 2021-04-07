using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour, IEnemyManager
{
    public List<EnemyCharacter> enemyList = new List<EnemyCharacter>();

    public int Level = 0;

    IEnemyGetter enemyGenerator;

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
