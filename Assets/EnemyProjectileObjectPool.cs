using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectileObjectPool : MonoBehaviour
{
    public List<GameObject> pooledBasicProjectiles = new List<GameObject>();
    public List<GameObject> pooledSecondProjecitles = new List<GameObject>();
    public GameObject BasicProjectilePrefab;
    public int BasicProjectileAmountToPool;


    private void Awake()
    {

    }

    void Start()
    {
        
        CreateProjectilePool(pooledBasicProjectiles, BasicProjectilePrefab);
    }

    private void CreateProjectilePool(List<GameObject> objectPoolList ,GameObject prefab)
    {
        GameObject tmp;
        for (int i = 0; i < BasicProjectileAmountToPool; i++)
        {
            tmp = Instantiate(prefab);
            tmp.SetActive(false);
            objectPoolList.Add(tmp);
        }
    }

    public GameObject RetrieveBasicProjectile()
    {
        for(int i = 0; i < pooledBasicProjectiles.Count; i++)
        {
            if (!pooledBasicProjectiles[i].activeInHierarchy)
            {
                return pooledBasicProjectiles[i];
            }
        }
        return null;
    }
}
