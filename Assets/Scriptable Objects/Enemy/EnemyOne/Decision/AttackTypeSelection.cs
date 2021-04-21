using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/Decisions/AttackTypeSelection")]
public class AttackTypeSelection : Decision
{
    public override bool Decide(EnemyStateController controller)
    {
        var cardType = controller._cardType;
        if (cardType == CardTypeSelected.Attack)
        {
            Debug.Log($"Decision detected that it card selected was spell damage");
            cardType = CardTypeSelected.None;
            Debug.Log($"Returning true and cardtype has been reset to {cardType}");
            return true;
        }
        else
            return false;      
    }
}
