using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectileHandler : MonoBehaviour
{ 
    public EnemyProjectileObjectPool EnemyProjectilePool;
    public List<Transform> BasicProjectileSpawnLocations = new List<Transform>();
    public bool isSpawningBasicProjectiles;
    private GameManager_BS gameManager_BS;
    private Transform playerTargetPos;

    private void Awake()
    {
        SetPlayerTargetPos();
    }

    void Start()
    {
        isSpawningBasicProjectiles = false;
    }

    void SetPlayerTargetPos()
    {
        gameManager_BS = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager_BS>();
        playerTargetPos = gameManager_BS.Player.gameObject.transform;
    }

    public void StartBasicProjectilesCoroutine(int ProjectileNumber, int timeBetweenProjectiles)
    {
        isSpawningBasicProjectiles = true;
        StartCoroutine(SpawnBasicProjectiles(ProjectileNumber, BasicProjectileSpawnLocations, timeBetweenProjectiles));
    }

    private IEnumerator SpawnBasicProjectiles(int projectileRetreivalNumber, List<Transform> projectileSpawnLocations, int timeBetweenProjectiles)
    {
        int n = 0;
        for (int i = 0; i < projectileRetreivalNumber; i++)
        {
            GameObject projectile = EnemyProjectilePool.RetrieveBasicProjectile();
            if (projectile != null)
            {
                SetPlayerAsTarget(projectile, playerTargetPos);

                n %= projectileSpawnLocations.Count;
                projectile.transform.position = projectileSpawnLocations[n].transform.position;
                
                projectile.SetActive(true);
                n++;
                yield return new WaitForSeconds(timeBetweenProjectiles);
            }
        }
        yield return new WaitForSeconds(3);
        isSpawningBasicProjectiles = false;
    }

    void SetPlayerAsTarget(GameObject projectile, Transform ProjectileTarget)
    {
        var projectileController = projectile.GetComponent<Enemy_projectile>();
        projectileController.targetPos = playerTargetPos;
    }
}
