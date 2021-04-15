using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/Decisions/Null")]
public class Decision_Null : Decision
{
    public override bool Decide(EnemyStateController controller)
    {
        return true;
    }
}
