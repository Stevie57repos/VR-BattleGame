using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/Decisions/printTestDecision")]
public class printTestDecision : Decision
{
    private bool test = false;

    public override bool Decide(EnemyStateController controller)
    {
        return test;
    }
    
    private bool CheckInt(EnemyStateController controller)
    {
        var enemyProjectileHandler = controller.enemyProjectileHandler;
        if (enemyProjectileHandler)
            return true;
        else
            return false;
    }
}
