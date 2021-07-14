﻿using UnityEngine;
public class GameManager_BS : MonoBehaviour
{
    public UI_Manager UImanager;

    public GameManagerEventChannelSO StartEvent;
    public GameManagerEventChannelSO BattleStartEvent;
    public GameManagerEventChannelSO LossEvent;
    public GameManagerEventChannelSO WonEvent;

    public GameManagerState currentGameManagerState;

    public readonly GameManager_State_Start startState = new GameManager_State_Start();
    public readonly GameManager_State_Battle battleState = new GameManager_State_Battle();
    public readonly GameManager_State_Won wonState = new GameManager_State_Won();
    public readonly GameManager_State_Loss lossState = new GameManager_State_Loss();

    private void OnEnable()
    {
        // triggered from player/enemy character health take damage methods
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
        TransitionToState(startState);      
    }

    public void TransitionToState(GameManagerState state)
    {
        currentGameManagerState = state;
        currentGameManagerState.EnterState(this);
    }

    public void TransitionToBattleLostState()
    {
        currentGameManagerState = lossState;
        currentGameManagerState.EnterState(this);
    }

    public void TransitionToBattleWonState()
    {
        currentGameManagerState = wonState;
        currentGameManagerState.EnterState(this);
    }

    public bool CheckIfInBattleState()
    {
        if(currentGameManagerState == battleState)
        {
            return true;
        }
        return false;
    }
}