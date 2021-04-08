using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class UI_Manager : MonoBehaviour, IUpdateUI
{
    public GameManager_BS gameManager;
    // Event needs a reference to the componenet. Needs to reference the player manager compononent
    public PlayerCharacter Player;

    #region Start State UI Variables
    // UI elements
    public GameObject Start_Button;

    #endregion

    #region Battle State UI Variables

    public GameObject BattleUIGO;

    // Player UI elements
    public GameObject PlayerHealthGO = null;
    public GameObject PlayerManaGO = null;

    // Enemy UI elements
    public GameObject EnemyHealthGO = null;
    public GameObject EnemyManaGO = null;

    #endregion

    #region Awake Methods

    private void Awake()
    {
        GameManagerCheck();
        PlayerCheck();

    }
    private void GameManagerCheck()
    {
        if (gameManager == null)
        {
            gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager_BS>();
            Debug.Log($"gameManager has been set to {gameManager.name}");
        }
    }

    private void PlayerCheck()
    {
        if(Player == null)
            Player = gameManager.Player.getGameObject().GetComponent<PlayerCharacter>();
    }

    #endregion

    #region OnEnable / Event Listeners

    private void OnEnable()
    {
        // listening for the GameManager to invoke Game Start
        gameManager.startState.OnGameStart += startUpdateUI;

        // listening for battle start
        gameManager.battleState.OnBattleStart += BattleUIStart;

        // listening for player damage or spell casting
        Player.OnDamage += EventUpdatePlayerHealthPanel;
        Player.OnSpell += EventUpdatePlayerManaPanel;
    }

    private void OnDisable()
    {
        gameManager.startState.OnGameStart -= startUpdateUI;

        gameManager.battleState.OnBattleStart -= BattleUIStart;

        Player.OnDamage -= EventUpdatePlayerHealthPanel;
        Player.OnSpell -= EventUpdatePlayerManaPanel;
    }

    #endregion

    private void Start()
    {
       
    }

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
        UpdatePlayerHealthPanel();
        UpdatePlayerManaPanel();

        //UpdateEnemyHealthPanel();
        //UpdateEnemyManaPanel();

        //UpdateAllPanels();
    }


    public void UpdateAllPanels()
    {
        UpdatePlayerHealthPanel();
        // Update Player Energy Panel
        UpdatePlayerManaPanel();
        // Update Enemy Health Panel
        UpdateEnemyHealthPanel();
        // Update Enemy Energy Panel
        UpdateEnemyManaPanel();
    }

    public void UpdatePlayerHealthPanel()
    {
        if( PlayerHealthGO != null)
        {
            // reference to the text mesh panel componenent
            var playerHealthPanel = PlayerHealthGO.GetComponent<TextMeshProUGUI>();

            Debug.Log($"gameManager player health is set to {gameManager.Player.Health}");
            // insert player health int
            playerHealthPanel.text = gameManager.Player.Health.ToString();

        }
    }

    void EventUpdatePlayerHealthPanel(int newHealth)
    {
        if( PlayerHealthGO != null)
        {
            // reference to the text mesh panel componenent
            var playerHealthPanel = PlayerHealthGO.GetComponent<TextMeshProUGUI>();

            playerHealthPanel.text = newHealth.ToString();
        }
    }

    private void UpdatePlayerManaPanel()
    {
        if(PlayerManaGO != null)
        {
            // reference to the text mesh panel componenent
            var playerManaPanel = PlayerManaGO.GetComponent<TextMeshProUGUI>();
            // insert player mana int
            playerManaPanel.text = gameManager.Player.Mana.ToString();
        }

    }

    void EventUpdatePlayerManaPanel(int newMana)
    {
        if(PlayerManaGO != null)
        {
            // reference to the text mesh panel componenent
            var playerManaPanel = PlayerManaGO.GetComponent<TextMeshProUGUI>();

            // insert player mana int
            playerManaPanel.text = newMana.ToString();

        }
    }

    public void UpdateEnemyHealthPanel()
    {
        if(EnemyHealthGO != null)
        {
            // reference to the text mesh panel componenent
            var enemyHealthPanel = EnemyHealthGO.GetComponent<TextMeshProUGUI>();
            // insert player health int
            Debug.Log($"gamemanger enemy health is {gameManager.Enemy.Health}");
            enemyHealthPanel.text = gameManager.Enemy.Health.ToString();
        }

    }
    public void UpdateEnemyManaPanel()
    {
        if (EnemyHealthGO != null)
        {
            // reference to the text mesh panel componenent
            var enemyManaPanel = EnemyManaGO.GetComponent<TextMeshProUGUI>();
            // insert player health int
            enemyManaPanel.text = gameManager.Enemy.Health.ToString();
        }
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
