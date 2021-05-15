using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/Decisions/AttackTypeSelection")]
public class AttackTypeSelection : Decision
{
    public override bool Decide(EnemyStateController controller)
    {
        var playerController = controller.PlayerControl;
        if (playerController.CardType == CardTypeSelected.Attack && playerController.CurrentStatus == PlayerStatus.Idle) 
        {
            Debug.Log($"Decision detected that it card selected was Attack Card");
            playerController.CardType = CardTypeSelected.None;
            playerController.CurrentStatus = PlayerStatus.InProgress;
            return true;
        }
        else
            return false;      
    }
}
