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
        StartProjectileAttack(controller);
    }

    private void StartProjectileAttack(EnemyStateController controller)
    {
        for( int i = 0; i < controller.currentState.BasicAttackDataList.Count; i++)
        {
            enemyProjectileHandler.BasicAttackData.Add(controller.currentState.BasicAttackDataList[i]);
            Debug.Log("card data added");
        }
        enemyProjectileHandler.StartAttackCoroutine();
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
