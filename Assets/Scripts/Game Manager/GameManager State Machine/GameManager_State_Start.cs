using System;

public class GameManager_State_Start : GameManagerState
{

    public override void Start(GameManager_BS gameManager)
    {

    }

    public override void EnterState(GameManager_BS gameManager)
    {
        gameManager.UImanager.UnloadPreviousScene();
        gameManager.UImanager.LoadMainMenu();
    }

    public override void Update(GameManager_BS gameManager)
    {

    }


}
