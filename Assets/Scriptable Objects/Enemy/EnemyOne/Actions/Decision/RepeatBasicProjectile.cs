using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/Decisions/RepeatBasicProjectile")]
public class RepeatBasicProjectile : Decision
{
    public EnemyProjectileHandler enemyProjectileHandler;
    public override bool Decide(EnemyStateController controller)
    {
        //if (enemyProjectileHandler == null)
        //    enemyProjectileHandler = controller.enemyProjectileHandler;
        //if (enemyProjectileHandler.CheckProjectileHandlerState())
        //{
        //    Debug.Log($" restart the loop");
        //    controller.currentState.EnterState(controller);
        //}
        //else
        //{
        //    Debug.Log("decision action is currently false");
        //}
        //return true;

        return true;
        
    }
}
