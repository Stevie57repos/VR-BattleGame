using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectileObjectPool : MonoBehaviour
{
    public GameObject BasicProjectilePrefab;
    public int BasicProjectileAmountToPool;
    public List<GameObject> pooledBasicProjectiles = new List<GameObject>();

    public GameObject MediumProjectilePrefab;
    public int MediumProjectileAmountToPool;
    public List<GameObject> pooledMediumProjecitles = new List<GameObject>();

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
