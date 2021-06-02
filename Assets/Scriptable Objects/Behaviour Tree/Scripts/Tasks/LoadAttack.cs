using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "T_BasicAttack", menuName = "BehaviorTree/Task/BasicAttack")]
public class LoadAttack : BehaviourNode
{
    public List<BasicAttackData> AttackDataList;
    [SerializeField] EnemyProjectileHandler _enemyProjectileHandler;
    [SerializeField] PlayerController _playerController;

    private void OnEnable()
    {
        _enemyProjectileHandler = null;
    }

    public override NodeState Evaluate()
    {
        if(_enemyProjectileHandler == null)
            _enemyProjectileHandler = _behaviourTree.BTBoard.GetValue<GameObject>("Owner").GetComponent<EnemyProjectileHandler>();
        if(_playerController == null)
            _playerController = _behaviourTree.BTBoard.GetValue<GameObject>("PlayerGO").GetComponent<PlayerController>();

        LoadAttackData();
        return _nodeState;
    }

    void LoadAttackData()
    {
        if (_enemyProjectileHandler.CurrentStateProjectileHandler == ProjectileHandlerState.Idle && _playerController.CurrentStatus == PlayerStatus.isIdle)
        {
            for (int i = 0; i < AttackDataList.Count; i++)
            {
                _enemyProjectileHandler.BasicAttackData.Add(AttackDataList[i]);
            }
            _enemyProjectileHandler.StartAttackCoroutine();
            _playerController.CurrentStatus = PlayerStatus.isInProgress;
            _nodeState = NodeState.Success;
        }
        else
        {
            _nodeState = NodeState.Failed;
        }
    }
}


