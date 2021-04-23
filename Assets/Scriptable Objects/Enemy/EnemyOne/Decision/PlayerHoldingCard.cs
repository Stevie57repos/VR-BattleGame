using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHoldingCard : Decision
{
    public override bool Decide(EnemyStateController controller)
    {
        bool _playerStatus = CheckForCardSelected(controller);
        return _playerStatus;
    }

    private bool CheckForCardSelected(EnemyStateController controller)
    {
        if (controller.PlayerControl.CardType != CardTypeSelected.None)
            return true;
        else
            return false;          
    }
}
