using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameManager_State_Battle : State
{
    public event Action<GameManager_BS> OnBattleStart;

    public override void Start(GameManager_BS gameManager)
    {

    }

    public override void EnterState(GameManager_BS gameManager)
    {
        OnBattleStart?.Invoke(gameManager);
        // enemyManager BattleEnemyStart should be called
        // UImanager BattleUIStart should be called
        // Deck Manager start
    }

    public override void Update(GameManager_BS gameManager)
    {

    }


}
