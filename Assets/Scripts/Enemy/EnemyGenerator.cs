using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGenerator : MonoBehaviour
{
    GameObject enemyPrefab;

    public void SetEnemySpawnPrefab(GameObject prefab)
    {
        enemyPrefab = prefab;
    }

    public GameObject SpawnEnemy()
    {
       GameObject enemyGO =  Instantiate(enemyPrefab);
       return enemyGO;
    }
}
