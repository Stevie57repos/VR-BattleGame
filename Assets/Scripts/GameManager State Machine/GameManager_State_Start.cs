using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Collections.Generic;

public class GameManager_State_Start : State
{
    public event Action<GameManager_BS> OnGameStart;

    public override void Start(GameManager_BS gameManager)
    {

    }

    public override void EnterState(GameManager_BS gameManager)
    {
        OnGameStart?.Invoke(gameManager);
    }

    public override void Update(GameManager_BS gameManager)
    {

    }


}
