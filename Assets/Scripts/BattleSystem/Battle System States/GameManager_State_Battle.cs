using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager_State_Battle : State
{

    public override void Start(GameManager_BS gameManager)
    {

    }

    public override void EnterState(GameManager_BS gameManager)
    {
        gameManager.enemyManager.BattleEnemyStart(gameManager);
        gameManager.UImanager.BattleUIStart(gameManager);
    }

    public override void Update(GameManager_BS gameManager)
    {

    }


}
