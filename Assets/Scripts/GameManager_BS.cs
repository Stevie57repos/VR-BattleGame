using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager_BS : MonoBehaviour
{
    // current state tracker
    State currentGameManagerState;

    // access to character data
    public Character_Base Player;
    public Character_Base Enemy;

    // different possible gameStates
    public readonly GameManager_State_Start startState = new GameManager_State_Start();
    public readonly GameManager_State_Battle battleState = new GameManager_State_Battle();
    public readonly GameManager_State_Won wonState = new GameManager_State_Won();
    public readonly GameManager_State_Loss lossState = new GameManager_State_Loss();

    //UI Manager Reference - Using interface to access
    public UI_Manager UImanager;
    public EnemyManager enemyManager;

    void Awake()
    {
        UImanager = GetComponent<UI_Manager>();
        enemyManager = GetComponent<EnemyManager>();
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
