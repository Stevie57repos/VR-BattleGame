using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/Decisions/DefenseTypeSelection")]
public class DefenseTypeSelection : Decision
{
    public override bool Decide(EnemyStateController controller)
    {
        var playerController = controller.PlayerControl;
        if (playerController.CardType == CardTypeSelected.Defend && playerController.CurrentStatus == PlayerStatus.isIdle)
        {
            Debug.Log($"Decision detected that it card selected was Attack Card");
            playerController.CardType = CardTypeSelected.None;
            playerController.CurrentStatus = PlayerStatus.isInProgress;
            return true;
        }
        else
            return false;
    }
}
