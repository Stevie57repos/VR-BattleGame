using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/Actions/TestAction")]
public class TestAction : Action
{
    public override void Act(EnemyStateController controller)
    {
        Debug.Log(" I am now doing the test action");
    }
}
