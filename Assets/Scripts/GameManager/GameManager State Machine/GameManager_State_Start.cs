using System;

public class GameManager_State_Start : GameManagerState
{
    public event Action<GameManager_BS> OnGameStart;

    public override void Start(GameManager_BS gameManager)
    {

    }

    public override void EnterState(GameManager_BS gameManager)
    {
        OnGameStart?.Invoke(gameManager);
        gameManager.GameManagerStartEvent.RaiseEvent();
        // ui manager
        // enemy manager
        // deck manager
    }

    public override void Update(GameManager_BS gameManager)
    {

    }


}
