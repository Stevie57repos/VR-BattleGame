using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SelectorNode", menuName = "BehaviorTree/Composite/SelectorNode")]
public class SelectorNode : CompositeNode
{
    public override NodeState Evaluate()
    {
        if(_activeNode == null)
        {
            if(_compTree.Count > 0)
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
                _activeNode = null;
                _nodeState= NodeState.Success;
                return _nodeState;

            case NodeState.Failed:
                if(_compTree.Count > 0)
                {
                    _activeNode = _compTree.Dequeue();
                    _activeNode.Initialize(_behaviourTree);                    
                }
                else
                {
                    _nodeState = NodeState.Failed;
                    return _nodeState;
                }
                break;
        }
        return _nodeState;      
    }
}
