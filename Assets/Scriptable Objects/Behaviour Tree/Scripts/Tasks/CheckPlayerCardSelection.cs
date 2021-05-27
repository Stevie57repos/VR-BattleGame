using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CheckCardSelection", menuName = "BehaviorTree/Task/CheckPlayerCardSelection")]
public class CheckPlayerCardSelection : BehaviourNode
{
    PlayerController _playerController;
    [SerializeField] CardTypeSelected _targetCardType;

    public override NodeState Evaluate()
    {

        if (_playerController == null)
            _playerController = _behaviourTree.BTBoard.GetValue<GameObject>("PlayerGO").GetComponent<PlayerController>();

        Debug.Log("checking player seleciton");
        if (_playerController.CardType == _targetCardType)
        {
            Debug.Log("Card selection matches and it is " + _targetCardType);
            _nodeState = NodeState.Success;          
        }
        else
        {
            Debug.Log("nothing selected or didn't match");
            _nodeState = NodeState.Failed;
        }
        return _nodeState;
    }
}
