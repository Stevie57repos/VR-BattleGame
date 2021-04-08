using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEnemyGetter 
{
    void SetEnemySpawnPrefab(GameObject EnemyPrefab);

    void SpawnEnemy();
}
