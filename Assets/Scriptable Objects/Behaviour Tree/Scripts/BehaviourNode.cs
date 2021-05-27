using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum NodeState
{
    Running,
    Success,
    Failed
}

public abstract class BehaviourNode : ScriptableObject
{
    protected NodeState _nodeState;
    protected BehaviourTree _behaviourTree;

    public NodeState NodeState => _nodeState;
    
    public virtual void Initialize(BehaviourTree behaviourTree)
    {
        _behaviourTree = behaviourTree;
        _nodeState = NodeState.Running;
    }

    public abstract NodeState Evaluate();
}
