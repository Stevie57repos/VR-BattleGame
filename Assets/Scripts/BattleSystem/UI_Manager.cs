using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class UI_Manager : MonoBehaviour, IUpdateUI
{
    private GameManager_BS gameManager;

    #region Start State UI Variables
    // UI elements
    public GameObject Start_Button;
    private TextMeshProUGUI Start_ButtonText;

    #endregion

    #region Battle State UI Variables

    public GameObject BattleUIGO;

    // Player UI elements
    public GameObject PlayerHealthGO;
    public GameObject PlayerManaGO;

    // Enemy UI elements
    public GameObject EnemyHealthGO;
    public GameObject EnemyManaGO;

    #endregion

    // Event needs a reference to the componenet. Needs to reference the player manager compononent
    PlayerCharacter Player;

    private void OnEnable()
    {
        GameManagerCheck();
        if(Player == null)
            Player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerCharacter>();
        Player.OnDamage += EventUpdatePlayerHealthPanel;
        Player.OnSpell += EventUpdatePlayerManaPanel;
    }

    private void OnDisable()
    {
        Player.OnDamage -= EventUpdatePlayerHealthPanel;
        Player.OnSpell -= EventUpdatePlayerManaPanel;
    }

    #region Start State Methods

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



    public void BattleUIStart(GameManager_BS gameManager)
    {
        GameManagerCheck();

        Start_Button.SetActive(false);
        BattleUIGO.SetActive(true);
        UpdateAllPanels();
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
        // reference to the text mesh panel componenent
        var playerHealthPanel = PlayerHealthGO.GetComponent<TextMeshProUGUI>();

        Debug.Log($"gameManager player health is set to {gameManager.Player.Health}");
        // insert player health int
        playerHealthPanel.text = gameManager.Player.Health.ToString();
    }

    void EventUpdatePlayerHealthPanel(int newHealth)
    {
        // reference to the text mesh panel componenent
        var playerHealthPanel = PlayerHealthGO.GetComponent<TextMeshProUGUI>();

        playerHealthPanel.text = newHealth.ToString();

    }

    private void UpdatePlayerManaPanel()
    {
        // reference to the text mesh panel componenent
        var playerManaPanel = PlayerManaGO.GetComponent<TextMeshProUGUI>();
        // insert player mana int
        playerManaPanel.text = gameManager.Player.Mana.ToString();
    }

    void EventUpdatePlayerManaPanel(int newMana)
    {
        // reference to the text mesh panel componenent
        var playerManaPanel = PlayerManaGO.GetComponent<TextMeshProUGUI>();

        // insert player mana int
        playerManaPanel.text = newMana.ToString();

    }

    public void UpdateEnemyHealthPanel()
    {
        // reference to the text mesh panel componenent
        var enemyHealthPanel = EnemyHealthGO.GetComponent<TextMeshProUGUI>();
        // insert player health int
        enemyHealthPanel.text = gameManager.Enemy.Health.ToString();
    }
    public void UpdateEnemyManaPanel()
    {
        // reference to the text mesh panel componenent
        var enemyManaPanel = EnemyManaGO.GetComponent<TextMeshProUGUI>();
        // insert player health int
        enemyManaPanel.text = gameManager.Enemy.Health.ToString();
    }



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

    private void GameManagerCheck()
    {
        if (gameManager == null)
        {
            gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager_BS>();
            Debug.Log($"gameManager has been set to {gameManager.name}");
        }
    }




}
