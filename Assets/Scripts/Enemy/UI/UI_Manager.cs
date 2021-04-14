using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class UI_Manager : MonoBehaviour, IUpdateUI
{
    // coroutine transition from state to state for tear down
    //ui break out into different components
    // objects register themselves
    // ai behavior list - ai behavior so - ai attack behavior
    // allows you to edit ai on run time

    public GameManager_BS gameManager;
    // Event needs a reference to the componenet. Needs to reference the player manager compononent
    public PlayerCharacter Player;
    private EnemyManager _enemyManager;

    #region Start State UI Variables
    // UI elements
    public GameObject Start_Button;

    #endregion

    #region Battle State UI Variables

    public GameObject BattleUIGO;

    // Player UI elements
    public GameObject PlayerHealthPanelGO = null;
    public GameObject PlayerManaPanelGO = null;

    // Enemy UI elements
    public GameObject EnemyHealthPanelGO = null;
    public GameObject EnemyManaPanelGO = null;

    public GameEvent BattleStart;

    #endregion

    #region Awake Methods

    private void Awake()
    {
        GameManagerCheck();
        PlayerCheck();
        _enemyManager = GetComponent<EnemyManager>();

    }
    private void GameManagerCheck()
    {
        if (gameManager == null)
        {
            gameManager = GetComponent<GameManager_BS>();
        }
    }

    private void PlayerCheck()
    {
        if (Player == null)
            Player = (PlayerCharacter)gameManager.Player;
    }

    #endregion

    #region OnEnable / Event Listeners

    private void OnEnable()
    {
        // listening for the GameManager to invoke Game Start
        gameManager.startState.OnGameStart += startUpdateUI;

        // listening for battle start
        gameManager.battleState.OnBattleStart += BattleUIStart;

    }

    private void OnDisable()
    {
        gameManager.startState.OnGameStart -= startUpdateUI;

        gameManager.battleState.OnBattleStart -= BattleUIStart;

    }

    #endregion


    #region Game Start Methods

    public void startUpdateUI(GameManager_BS gameManager)
    {
        this.gameManager = gameManager;

        // set start menu button text
        SetButtonText(Start_Button, "Battle");

        // set button activation method
        SetButtonActivation(Start_Button);

        // Ensure that the battle UI is set to inactive
        BattleUIGO.SetActive(false);
    }

    void SetButtonActivation(GameObject buttonGO)
    {
        Button StartButton = buttonGO.GetComponent<Button>();
        StartButton.onClick.AddListener(transitionToBattleState);
    }

    public void transitionToBattleState()
    {
        gameManager.TransitionToState(gameManager.battleState);
    }

    #endregion


    #region Game Battle Start Methods

    public void BattleUIStart(GameManager_BS gameManager)
    {
        Start_Button.SetActive(false);
        BattleUIGO.SetActive(true);
        BattleStart.Raise();
    }




    #endregion


    /// <summary>
    /// SetButtonText takes a gameobject with a button on it and a string. String will be inserted into the button
    /// </summary>
    /// <param name="button"></param>
    /// <param name="text"></param>
    public void SetButtonText(GameObject button, string text)
    {
        TextMeshProUGUI buttonTextMesh = button.GetComponentInChildren<TextMeshProUGUI>();
        buttonTextMesh.text = text;
    }





}
