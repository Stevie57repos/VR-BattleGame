using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CompositeNode : BehaviourNode
{
    [SerializeField] protected List<BehaviourNode> _behaviourNodes;

    protected Queue<BehaviourNode> _compTree;
    protected BehaviourNode _activeNode;

    public override void Initialize(BehaviourTree behaviourTree)
    {
        base.Initialize(behaviourTree);
        _activeNode = null;

        _compTree = new Queue<BehaviourNode>();

        for(int i = 0; i < _behaviourNodes.Count; i++)
        {
            _compTree.Enqueue(_behaviourNodes[i]);
        }
    }
}
