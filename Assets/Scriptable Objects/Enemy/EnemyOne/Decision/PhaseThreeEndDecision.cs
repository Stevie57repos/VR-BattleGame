using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/Decisions/PhaseThreeEndDecision")]
public class PhaseThreeEndDecision : Decision
{
    private EnemyCharacter _enemyCharacter;
    public override bool Decide(EnemyStateController controller)
    {
        bool healthCheck = EvaluateCurrentHealth(controller);
        return healthCheck;
    }

    private bool EvaluateCurrentHealth(EnemyStateController controller)
    {
        if(_enemyCharacter == null)
        {
            _enemyCharacter = controller._enemyCharacter;
        }

        if (_enemyCharacter.Health <= 0)
            return true;
        else
            return false;
    }
}
