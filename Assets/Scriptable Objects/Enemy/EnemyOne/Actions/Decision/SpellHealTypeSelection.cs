using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/Decisions/SpellHealTypeSelection")]
public class SpellHealTypeSelection : Decision
{
    public override bool Decide(EnemyStateController controller)
    {
        var playerController = controller.PlayerControl;
        if (playerController.CardType == CardTypeSelected.SpellHeal && playerController.CurrentStatus == PlayerStatus.isIdle)
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
