using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager_State_Start : State
{
    public override void Start(GameManager_BS gameManager)
    {

    }

    public override void EnterState(GameManager_BS gameManager)
    {   
        gameManager.UImanager.startUpdateUI(gameManager);
    }

    public override void Update(GameManager_BS gameManager)
    {

    }


}
