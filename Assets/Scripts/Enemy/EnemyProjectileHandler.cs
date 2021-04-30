using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ProjectileHandlerState { Idle, InProgress, Complete }

public class EnemyProjectileHandler : MonoBehaviour
{ 
    public ProjectileHandlerState CurrentStateProjectileHandler;

    public EnemyProjectileObjectPool EnemyProjectilePool;
    public List<Transform> BasicProjectileSpawnLocations = new List<Transform>();

    public List<BasicAttackData> BasicAttackData = new List<BasicAttackData>();

    public CountdownTimer Timer;

    private Transform playerTargetPos;
    [SerializeField] CharacterRegistry _characterRegistry;
    
    private void Awake()
    {
        SetPlayerTargetPos();
    }

    void Start()
    {
        CurrentStateProjectileHandler = ProjectileHandlerState.Idle;
    }

    void SetPlayerTargetPos()
    {
        playerTargetPos = _characterRegistry.Player.transform;
    }


    public void StartAttackCoroutine()
    {
        CurrentStateProjectileHandler = ProjectileHandlerState.InProgress;
        StartCoroutine(AttackCoroutine());
    }

    public IEnumerator AttackCoroutine()
    {
        yield return StartCoroutine(AttackListCoroutine());
        Debug.Log("This should appear last in the experiment");
        yield return new WaitForSeconds(1);
        CurrentStateProjectileHandler = ProjectileHandlerState.Complete;
    }

    public IEnumerator AttackListCoroutine()
    {
        for (int i = 0; i < BasicAttackData.Count; i++)
        {
            Debug.Log("Firing attack " + i);
            yield return new WaitForSeconds(BasicAttackData[i].WaitBeforeAttackBegins);
            yield return StartCoroutine(SpawnBasicProjectiles(BasicAttackData[i].ProjectileNumber, BasicProjectileSpawnLocations, BasicAttackData[i].TimeBetweenProjectiles));
            Debug.Log($"Waiting time before next attack is {BasicAttackData[i].WaitAfterAttackEnds}");
            yield return new WaitForSeconds(BasicAttackData[i].WaitAfterAttackEnds);
        }
    }

    public void StartBasicProjectilesCoroutine(int ProjectileNumber, float timeBetweenProjectiles)
    {
        CurrentStateProjectileHandler = ProjectileHandlerState.InProgress;
        Debug.Log("Projectile hander is in progress state");
        StartCoroutine(SpawnBasicProjectiles(ProjectileNumber, BasicProjectileSpawnLocations, timeBetweenProjectiles));
    }

    public IEnumerator SpawnBasicProjectiles(int projectileRetreivalNumber, List<Transform> projectileSpawnLocations, float timeBetweenProjectiles)
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
            }
        }
    }

    void SetPlayerAsTarget(GameObject projectile, Transform ProjectileTarget)
    {
        var projectileController = projectile.GetComponent<Enemy_projectile>();
        projectileController.targetPos = playerTargetPos;
    }

}
