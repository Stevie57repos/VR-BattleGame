using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "SequenceNode", menuName = "BehaviorTree/Composite/SequenceNode")]
public class SequenceNode : CompositeNode
{
    public override NodeState Evaluate()
    {
        if(_activeNode == null)
        {
            if(_compTree.Count >0)
            {
                _activeNode = _compTree.Dequeue();
                _activeNode.Initialize(_behaviourTree);
            }
        }

        switch (_activeNode.Evaluate())
        {
            case NodeState.Running:
                _nodeState = NodeState.Running;
                break;

            case NodeState.Success:
                if(_compTree.Count > 0)
                {
                    _activeNode = _compTree.Dequeue();
                    _activeNode.Initialize(_behaviourTree);
                }
                else
                {
                    _activeNode = null;
                    _nodeState = NodeState.Success;
                    return _nodeState;
                }
                break;

            case NodeState.Failed:
                _nodeState = NodeState.Failed;
                return _nodeState;
        }
        return _nodeState;
    }

}
