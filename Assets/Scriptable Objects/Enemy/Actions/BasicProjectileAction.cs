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

    private void SpawnBasicProjectiles(EnemyStateController controller)
    {
        if (enemyProjectileHandler.isSpawningBasicProjectiles == false)
        {
            enemyProjectileHandler.StartBasicProjectilesCoroutine(2, 2);
        }
    }

    private void CheckEnemyProjectileHandler(EnemyStateController controller)
    {
        if (enemyProjectileHandler == null)
        {
            enemyProjectileHandler = controller.enemyProjectileHandler;
        }
    }
}
