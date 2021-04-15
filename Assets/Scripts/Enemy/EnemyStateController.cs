using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateController : MonoBehaviour
{
    public EnemyCharacter _enemyCharacter;
    // set intial state in inspector
    public EnemyState currentState;
    public EnemyManager enemyManager;
    public EnemyProjectileHandler enemyProjectileHandler;
    public EnemyState maintainState;
    public GameObject playerTarget;


    private void Awake()
    {
        _enemyCharacter = GetComponent<EnemyCharacter>();
        enemyProjectileHandler = GetComponent<EnemyProjectileHandler>();

    }

    void Start()
    {
        enemyProjectileHandler.EnemyProjectilePool = enemyManager.EnemyProjectilePool;

    }

    void Update()
    {
        currentState.UpdateStateActions(this);
    }
    public void TransitionToState(EnemyState nextState)
    {
        if(nextState != maintainState)
        {
            currentState = nextState;
        }
    }
}
