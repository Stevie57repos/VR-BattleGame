using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/Decisions/PhaseOneEndDecision")]
public class PhaseOneEndDecision : Decision
{
    private EnemyCharacter _enemyCharacter;
    public int healthThreshold;
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

        if(healthThreshold == 0)
            healthThreshold = (int)(_enemyCharacter.MaxHealth * 0.75);

        if (_enemyCharacter.Health <= (healthThreshold))
            return true;
        else
            return false;
    }
}
