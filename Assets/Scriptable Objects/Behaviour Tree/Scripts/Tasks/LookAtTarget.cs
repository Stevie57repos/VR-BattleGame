using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LookAt", menuName = "BehaviorTree/Task/LookAt")]
public class LookAtTarget : BehaviourNode
{
    public override NodeState Evaluate()
    {
        GameObject target = _behaviourTree.BTBoard.GetValue<GameObject>("PlayerGO");
        GameObject owner = _behaviourTree.BTBoard.GetValue<GameObject>("Owner");

        if (target != null)
        {
            owner.transform.LookAt(target.transform);
            _nodeState = NodeState.Success;
        }
        else
        {
            _nodeState = NodeState.Failed;
        }
        return _nodeState;
    }
}
