using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CheckCardSelection", menuName = "BehaviorTree/Task/CheckPlayerCardSelection")]
public class CheckPlayerCardSelection : BehaviourNode
{
    [SerializeField] PlayerController _playerController;
    [SerializeField] CardTypeSelected _targetCardType;

    private void OnEnable()
    {
        _playerController = null;
    }

    private void Awake()
    {
        CheckPlayerController();
    }
    void CheckPlayerController()
    {
        if(_playerController == null)
            _playerController = _behaviourTree.BTBoard.GetValue<GameObject>("PlayerGO").GetComponent<PlayerController>();
    }

    public override NodeState Evaluate()
    {
        if (_playerController == null)
        {
            _playerController = _behaviourTree.BTBoard.GetValue<GameObject>("PlayerGO").GetComponent<PlayerController>();
        }

        if (_playerController.CardType == _targetCardType)
        {
            _nodeState = NodeState.Success;          
        }
        else
        {
            _nodeState = NodeState.Failed;
        }
        return _nodeState;
    }
}
