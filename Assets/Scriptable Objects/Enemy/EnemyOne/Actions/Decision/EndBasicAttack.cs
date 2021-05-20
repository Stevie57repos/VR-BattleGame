using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/Decisions/EndBasicAttack")]
public class EndBasicAttack : Decision
{
    EnemyProjectileHandler enemyProjectileHandler;
    public override bool Decide(EnemyStateController controller)
    {
        CheckEnemyProjectileHandler(controller);
        return CheckEnemyProjectileHandlerStatus(controller);
    }
    private void CheckEnemyProjectileHandler(EnemyStateController controller)
    {
        if (enemyProjectileHandler == null)
        {
            enemyProjectileHandler = controller.enemyProjectileHandler;
        }
    }

    private bool CheckEnemyProjectileHandlerStatus(EnemyStateController controller)
    {
        if (enemyProjectileHandler.CurrentStateProjectileHandler == ProjectileHandlerState.Complete)
        {
            enemyProjectileHandler.CurrentStateProjectileHandler = ProjectileHandlerState.Idle;     
            return true;
        }
        else
            return false;
    }
}
