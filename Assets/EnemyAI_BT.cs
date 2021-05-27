using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI_BT : MonoBehaviour
{
    private BehaviourTree _behaviourTree;

    void Start()
    {
        _behaviourTree = GetComponent<BehaviourTree>();
        _behaviourTree.BeginTree();
    }

    void Update()
    {
        
    }
}
