using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectileObjectPool : MonoBehaviour
{
    public List<GameObject> pooledBasicProjectiles;
    public GameObject BasicProjectile;
    public int BasicProjectileAmountToPool;

    void Start()
    {
        CreateProjectilePool(pooledBasicProjectiles, BasicProjectile);
    }

    private void CreateProjectilePool(List<GameObject> gameObjectList, GameObject gameobjectToPool)
    {
        if(gameObjectList != null && gameobjectToPool != null)
        {
            gameObjectList = new List<GameObject>();
            GameObject tmp;
            for (int i = 0; i < BasicProjectileAmountToPool; i++)
            {
                tmp = Instantiate(gameobjectToPool);
                tmp.SetActive(false);
                pooledBasicProjectiles.Add(gameobjectToPool);
            }
        }
        else         
            Debug.Log($" Enemy projectile prefab and list parameters are null");        
    }
}
