using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "PluggableAI/Actions/BasicProjectile")]
public class BasicProjectileAction : Action
{
    EnemyProjectileHandler enemyProjectileHandler;

    public override void Act(EnemyStateController controller)
    {
        CheckEnemyProjectileHandler(controller);
        SpawnBasicProjectiles(controller);
    }
    private void CheckEnemyProjectileHandler(EnemyStateController controller)
    {
        if (enemyProjectileHandler == null)
        {
            enemyProjectileHandler = controller.enemyProjectileHandler;
        }
    }
    private void SpawnBasicProjectiles(EnemyStateController controller)
    {
        Debug.Log("firing spawn basic couroutine");
        if(controller.enemyProjectileHandler.CurrentStateProjectileHandler == ProjectileHandlerState.Idle)
        {
            controller.enemyProjectileHandler.CurrentStateProjectileHandler = ProjectileHandlerState.InProgress;
            enemyProjectileHandler.StartBasicProjectilesCoroutine(3, 3);
        }
 
    }
}
