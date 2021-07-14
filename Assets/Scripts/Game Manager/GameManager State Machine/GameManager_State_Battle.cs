using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameManager_State_Battle : GameManagerState
{
    //public event Action<GameManager_BS> OnBattleStart;

    public override void Start(GameManager_BS gameManager)
    {

    }

    public override void EnterState(GameManager_BS gameManager)
    {
        gameManager.UImanager.UnloadPreviousScene();
        gameManager.UImanager.LoadBattleMenuUI();
    }

    public override void Update(GameManager_BS gameManager)
    {

    }
}
