using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ProjectileHandlerState { Idle, InProgress, Complete }

public class EnemyProjectileHandler : MonoBehaviour
{ 

    public ProjectileHandlerState CurrentStateProjectileHandler;

    public EnemyProjectileObjectPool EnemyProjectilePool;
    public List<Transform> BasicProjectileSpawnLocations = new List<Transform>();

    private GameManager_BS gameManager_BS;
    private Transform playerTargetPos;
    
    private void Awake()
    {
        SetPlayerTargetPos();
    }

    void Start()
    {
        Debug.Log($"current state projectile handler is in idle {CurrentStateProjectileHandler}");
        CurrentStateProjectileHandler = ProjectileHandlerState.Idle;
    }

    void SetPlayerTargetPos()
    {
        gameManager_BS = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager_BS>();
        playerTargetPos = gameManager_BS.Player.gameObject.transform;
    }

    public void StartBasicProjectilesCoroutine(int ProjectileNumber, int timeBetweenProjectiles)
    {
        CurrentStateProjectileHandler = ProjectileHandlerState.InProgress;
        Debug.Log("Projectile hander is in progress state");
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
                projectile.transform.rotation = Quaternion.identity;
                projectile.SetActive(true);
                n++;
                yield return new WaitForSeconds(timeBetweenProjectiles);
                Debug.Log($"yield wait for {timeBetweenProjectiles} : time between projectiles has been called");
            }
        }
        yield return new WaitForSeconds(1);
        CurrentStateProjectileHandler = ProjectileHandlerState.Complete;
    }

    //public bool CheckProjectileHandlerState()
    //{
    //    if (CurrentStateProjectileHandler == ProjectileHandlerState.Idle || CurrentStateProjectileHandler == ProjectileHandlerState.Complete)
    //        return true;
    //    else
    //        return false;
    //}

    void SetPlayerAsTarget(GameObject projectile, Transform ProjectileTarget)
    {
        var projectileController = projectile.GetComponent<Enemy_projectile>();
        projectileController.targetPos = playerTargetPos;
    }
}
