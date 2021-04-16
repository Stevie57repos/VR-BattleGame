using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameManager_State_Battle : GameManagerState
{
    public event Action<GameManager_BS> OnBattleStart;

    public override void Start(GameManager_BS gameManager)
    {

    }

    public override void EnterState(GameManager_BS gameManager)
    {
        BattleStart(gameManager);
        // enemyManager BattleEnemyStart should be called
        // UImanager BattleUIStart should be called
        // Deck Manager start
    }

    public override void Update(GameManager_BS gameManager)
    {

    }

    void BattleStart(GameManager_BS gameManager)
    {
        Debug.Log($"Battle start event invoke has been called");
        OnBattleStart?.Invoke(gameManager);
    }


}
