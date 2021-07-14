using UnityEngine;
public class GameManager_BS : MonoBehaviour
{
    public UI_Manager UImanager;
    public GameManagerState CurrentGameManagerState;

    [Header("Events")]
    public GameManagerEventChannelSO StartEvent;
    public GameManagerEventChannelSO BattleStartEvent;
    public GameManagerEventChannelSO LossEvent;
    public GameManagerEventChannelSO WonEvent;

    public readonly GameManager_State_Start StartState = new GameManager_State_Start();
    public readonly GameManager_State_Battle BattleState = new GameManager_State_Battle();
    public readonly GameManager_State_Won WonState = new GameManager_State_Won();
    public readonly GameManager_State_Loss LossState = new GameManager_State_Loss();
    private void OnEnable()
    {
        LossEvent.GameManagerEvent += TransitionToBattleLostState;
        WonEvent.GameManagerEvent += TransitionToBattleWonState;
    }
    private void OnDisable()
    {
        LossEvent.GameManagerEvent -= TransitionToBattleLostState;
        WonEvent.GameManagerEvent -= TransitionToBattleWonState;
    }

    private void Awake()
    {
        UImanager = GetComponent<UI_Manager>();
    }

    void Start()
    {
        TransitionToState(StartState);      
    }

    public void TransitionToState(GameManagerState state)
    {
        CurrentGameManagerState = state;
        CurrentGameManagerState.EnterState(this);
    }

    public void TransitionToBattleLostState()
    {
        CurrentGameManagerState = LossState;
        CurrentGameManagerState.EnterState(this);
    }

    public void TransitionToBattleWonState()
    {
        CurrentGameManagerState = WonState;
        CurrentGameManagerState.EnterState(this);
    }

    public bool CheckIfInBattleState()
    {
        if(CurrentGameManagerState == BattleState)
        {
            return true;
        }
        return false;
    }
}
