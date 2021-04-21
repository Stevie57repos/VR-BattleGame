using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager_BS : MonoBehaviour
{
    private static GameManager_BS _instance;
    public static GameManager_BS Instance
    {
        get
        {
            if (_instance == null)
            {
                Debug.LogError("The CommandManager is NULL");
            }
            return _instance;
        }
    }

    public GameManagerStartEventChannelSO GameManagerStartEvent;


    // access to character stats componenent
    public Character_Base Player;
    public Character_Base Enemy;

    // current state tracker
    GameManagerState currentGameManagerState;

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
        SetGameManagerInstance();
        UImanager = GetComponent<UI_Manager>();
        enemyManager = GetComponent<EnemyManager>();
    }

    void SetGameManagerInstance()
    {
        _instance = this;
    }

    public void CheckGameManagerInstance()
    {
        if (_instance == null) Debug.Log("its still null");
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

    private void Update()
    {
        CheckGameManagerInstance();
    }
}
