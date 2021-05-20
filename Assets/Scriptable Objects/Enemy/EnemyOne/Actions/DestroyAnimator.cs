using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/Actions/DestroyAnimator")]
public class DestroyAnimator : Action
{
    
    public override void Act(EnemyStateController controller)
    {
        Destroy(controller.CubeBossAnimator);
    }
}
