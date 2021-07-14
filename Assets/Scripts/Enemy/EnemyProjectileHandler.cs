using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public enum ProjectileHandlerState { Idle, InProgress, Complete }

public class EnemyProjectileHandler : MonoBehaviour
{ 
    public ProjectileHandlerState CurrentStateProjectileHandler;
    public EnemyProjectileObjectPool EnemyProjectilePool;

    public List<Transform> BasicProjectileSpawnLocations = new List<Transform>();
    public GameObject[] Portals = new GameObject[3];
    public GameObject PortalPrefab;


    public List<BasicAttackData> BasicAttackData = new List<BasicAttackData>();
    public List<BasicAttackData> LeftProjectileList = new List<BasicAttackData>();
    public List<BasicAttackData> RightProjectileList = new List<BasicAttackData>();
    public List<BasicAttackData> TopProjectileList = new List<BasicAttackData>();
    public List<BasicAttackData> AttackOrder = new List<BasicAttackData>();

    public static readonly int AttackLeft = Animator.StringToHash("Attack_Left");
    public static readonly int AttackRight = Animator.StringToHash("Attack_Right");
    public static readonly int AttackTop = Animator.StringToHash("Attack_Top");
    public static readonly int DamageHit = Animator.StringToHash("Damage_Hit");

    public CountdownTimer Timer;

    private Transform playerTargetPos;
    [SerializeField] CharacterRegistry _characterRegistry;
    private Animator _enemyAnimator;

    private void Awake()
    {
        SetPlayerTargetPos();
        _enemyAnimator = GetComponent<Animator>();

        if(EnemyProjectilePool == null)
        {
            EnemyProjectilePool = GameObject.FindGameObjectWithTag("GameManager").GetComponent<EnemyProjectileObjectPool>();
        }
    }
    void SetPlayerTargetPos()
    {
        playerTargetPos = _characterRegistry.Player.transform;
    }

    void Start()
    {
        CurrentStateProjectileHandler = ProjectileHandlerState.Idle;
    }

    public void StartAttackCoroutine()
    {
        CurrentStateProjectileHandler = ProjectileHandlerState.InProgress;
        SortAttackData();
        BasicAttackData.Clear();
        StartCoroutine(BeginAttackAnimationCoroutine());
    }
    void SortAttackData()
    {
        BasicAttackData currentAttackData = null;

        for (int i = 0; i < BasicAttackData.Count; i++)
        {
            if (currentAttackData == null)
            {
                currentAttackData = BasicAttackData[0];
                AttackOrder.Add(BasicAttackData[0]);
            }
            else if(currentAttackData.SpawnLocation != BasicAttackData[i].SpawnLocation)
            {
                AttackOrder.Add(BasicAttackData[i]);
            }

            switch (BasicAttackData[i].SpawnLocation)
            {
                case SpawnLoactions.Left:
                    LeftProjectileList.Add(BasicAttackData[i]);                        
                    break;
                case SpawnLoactions.Right:
                    RightProjectileList.Add(BasicAttackData[i]);
                    break;
                case SpawnLoactions.Top:
                    TopProjectileList.Add(BasicAttackData[i]);
                    break;
            }                     
        }
    }

    public IEnumerator BeginAttackAnimationCoroutine()
    {
        yield return StartCoroutine(AttackAnimationCoroutine());
        AttackOrder.Clear();
    }

    public IEnumerator AttackAnimationCoroutine()
    {
        WaitForSeconds waitBetweenAnimations = new WaitForSeconds(3.5f);
        for (int i = 0; i < AttackOrder.Count; i++)
        {
            switch (AttackOrder[i].SpawnLocation)
            {
                case SpawnLoactions.Left:
                    _enemyAnimator.SetTrigger("Attack_Left");                   
                    break;
                case SpawnLoactions.Right:
                    _enemyAnimator.SetTrigger("Attack_Right");
                    break;
                case SpawnLoactions.Top:
                    _enemyAnimator.SetTrigger("Attack_Top");
                    break;
            }
            yield return waitBetweenAnimations;
        }

        CurrentStateProjectileHandler = CheckForStateMachine() ? ProjectileHandlerState.Complete : ProjectileHandlerState.Idle;            
    }

    bool CheckForStateMachine()
    {
        EnemyStateController stateMachine = GetComponent<EnemyStateController>();
        if (stateMachine != null)
            return true;
        else
            return false;
    }


    public void LeftProjectileAttack()
    {
        StartCoroutine(AttackCoroutine(LeftProjectileList, BasicProjectileSpawnLocations[0], Portals[0], PortalPrefab));
        Debug.Log($"Left projectile attack called");
    }

    public void TopProjectileAttack()
    {
        StartCoroutine(AttackCoroutine(TopProjectileList, BasicProjectileSpawnLocations[1], Portals[1], PortalPrefab));
        Debug.Log($"Top projectile attack called");
    }

    public void RightProjectileAttack()
    {
        if (RightProjectileList.Count != 0)
          StartCoroutine(AttackCoroutine(RightProjectileList, BasicProjectileSpawnLocations[2], Portals[2], PortalPrefab));
        Debug.Log($"right projectile attack called");
    }

    public IEnumerator AttackCoroutine(List<BasicAttackData> projectileList, Transform spawnLocation, GameObject portal, GameObject prefab)
    {
        GameObject portalGO = Instantiate(PortalPrefab);
        portalGO.transform.position = portal.transform.position;

        for (int i = 0; i < projectileList.Count; i++)
        {
            yield return new WaitForSeconds(projectileList[i].WaitBeforeAttackBegins);
            yield return StartCoroutine(SpawnBasicProjectiles(projectileList[i].ProjectileNumber, portalGO.transform, projectileList[i].TimeBetweenProjectiles));
        }
        projectileList.Clear();
        Destroy(portalGO);
    }

    public IEnumerator SpawnBasicProjectiles(int projectileRetreivalNumber, Transform projectileSpawnLocation, float timeBetweenProjectiles)
    {
        for (int i = 0; i < projectileRetreivalNumber; i++)
        {
            GameObject projectile = EnemyProjectilePool.RetrieveBasicProjectile();
            if (projectile != null)
            {
                SetPlayerAsTarget(projectile, playerTargetPos);
                projectile.transform.position = projectileSpawnLocation.transform.position;
                projectile.transform.rotation = Quaternion.identity;
                projectile.SetActive(true);              
                yield return new WaitForSeconds(timeBetweenProjectiles);
            }
            else
            {
                Debug.Log("projectile retrieval returned null");
            }
        }
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
