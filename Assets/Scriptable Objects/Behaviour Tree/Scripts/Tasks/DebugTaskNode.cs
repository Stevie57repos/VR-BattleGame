using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DebugLog", menuName = "BehaviorTree/Task/DebugLog")]
public class DebugTaskNode : BehaviourNode
{
    public override NodeState Evaluate()
    {
        _nodeState = NodeState.Success;
        Debug.Log("debug log node was a success");
        return _nodeState;
    }
}
