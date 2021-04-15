using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitAction : Action
{
    float currentTime = 0f;
    float startingTime = 10f;
    
    public override void Act(EnemyStateController controller)
    {
        WaitTwoSeconds(controller);
    }

    private void WaitTwoSeconds(EnemyStateController controller)
    {
        
    }


}
