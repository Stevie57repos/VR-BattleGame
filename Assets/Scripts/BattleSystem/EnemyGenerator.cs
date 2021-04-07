using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGenerator : MonoBehaviour, IEnemyGetter
{
    GameObject enemyPrefab;

    public void SetEnemySpawnPrefab(GameObject prefab)
    {
        enemyPrefab = prefab;
        Debug.Log($"enemy prefab is {enemyPrefab.name}");
    }

    public void SpawnEnemy()
    {
        Instantiate(enemyPrefab);
    }


}
