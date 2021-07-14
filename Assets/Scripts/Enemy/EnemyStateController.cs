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
    public GameObject CubeModelTargetGO;
    public EnemyState isDeadState;

    private EnemyRewardController  _enemyRewardController;
    public GameManagerEventChannelSO BattleFinish;

    private void Awake()
    {
        _enemyCharacter = GetComponent<EnemyCharacter>();
        enemyProjectileHandler = GetComponent<EnemyProjectileHandler>();
        PlayerControl = CharacterRegistry.Player.GetComponent<PlayerController>();
        Timer = GetComponent<CountdownTimer>();
        CubeBossAnimator = GetComponent<Animator>();
        _enemyRewardController = GetComponent<EnemyRewardController>();
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
    public void EnterDeadState()
    {
        TransitionToState(isDeadState);
        Destroy(this.gameObject, 3.5f);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(currentState == isDeadState)
        {
            BattleFinish.RaiseEvent();
            _enemyRewardController.TriggerRewards();
            Destroy(this.gameObject);
        }
    }
}
