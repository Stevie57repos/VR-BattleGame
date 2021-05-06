using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class EnemyStateController : MonoBehaviour
{
    public EnemyManager enemyManager;
    public EnemyCharacter _enemyCharacter;
    public EnemyProjectileHandler enemyProjectileHandler;
    // set intial state in inspector
    public EnemyState currentState;
    public EnemyState maintainState;
    public CharacterRegistry CharacterRegistry;
    public GameObject playerTarget;
    public PlayerController PlayerControl;
    public CountdownTimer Timer;
    public Animator CubeBossAnimator;

    private void Awake()
    {
        _enemyCharacter = GetComponent<EnemyCharacter>();
        enemyProjectileHandler = GetComponent<EnemyProjectileHandler>();
        PlayerControl = CharacterRegistry.Player.GetComponent<PlayerController>();
        Timer = GetComponent<CountdownTimer>();
        CubeBossAnimator = GetComponent<Animator>();
    }

    void Start()
    {
        enemyProjectileHandler.EnemyProjectilePool = enemyManager.EnemyProjectilePool;
        currentState.EnterState(this);
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
            currentState?.EnterState(this);
        }
    }
}
