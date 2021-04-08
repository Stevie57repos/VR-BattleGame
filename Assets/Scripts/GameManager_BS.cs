using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager_BS : MonoBehaviour
{
    // current state tracker
    State currentGameManagerState;

    // access to character data
    public ICharacter Player;
    public ICharacter Enemy;

    // different possible gameStates
    public readonly GameManager_State_Start startState = new GameManager_State_Start();
    public readonly GameManager_State_Battle battleState = new GameManager_State_Battle();
    public readonly GameManager_State_Won wonState = new GameManager_State_Won();
    public readonly GameManager_State_Loss lossState = new GameManager_State_Loss();

    //UI Manager Reference - Using interface to access
    public IUpdateUI UImanager;
    public IEnemyManager enemyManager;

    void Awake()
    {
        Player = GameObject.FindGameObjectWithTag("Player").GetComponent<ICharacter>();
        UImanager = GetComponent<IUpdateUI>();
        enemyManager = GetComponent<IEnemyManager>();
    }

    void Start()
    {


        // Set state to start
        TransitionToState(startState);      
    }

    public void TransitionToState(State state)
    {
        currentGameManagerState = state;
        currentGameManagerState.EnterState(this);
    }

}
