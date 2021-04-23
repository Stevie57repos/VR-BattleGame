using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/Decisions/IdleTimerEnd")]
public class IdleTimerEnd : Decision
{
    public override bool Decide(EnemyStateController controller)
    {
        bool _timerStatus = CheckIdleTimer(controller);
        return _timerStatus;
    }

    private bool CheckIdleTimer(EnemyStateController controller)
    {
        if (controller.Timer.isCountDownComplete == true)
            return true;
        else
            return false;
    }
}
