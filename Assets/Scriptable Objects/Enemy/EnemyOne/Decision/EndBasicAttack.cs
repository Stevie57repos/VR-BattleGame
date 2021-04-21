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
            Debug.Log($"enemy projectile handler status is complete");
            enemyProjectileHandler.CurrentStateProjectileHandler = ProjectileHandlerState.Idle;
            controller._cardType = CardTypeSelected.None;
            Debug.Log($"enemy projectile is complete and handler status is resetting and is now {enemyProjectileHandler.CurrentStateProjectileHandler}");
            return true;
        }
        else
            return false;
    }
}
