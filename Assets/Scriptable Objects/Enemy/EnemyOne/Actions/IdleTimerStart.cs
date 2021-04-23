using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/Actions/IdleTimer")]
public class IdleTimerStart : Action
{
    public override void Act(EnemyStateController controller)
    {
        StartTimer(controller, 10);
    }

    private void StartTimer(EnemyStateController controller, int CountDown)
    {
        controller.Timer.StartTimer(CountDown);
    }
}
