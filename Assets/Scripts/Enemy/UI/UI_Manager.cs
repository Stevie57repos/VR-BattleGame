using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using UnityEngine.SceneManagement;

public class UI_Manager : MonoBehaviour
{
    public GameManager_BS gameManager;
    public PlayerCharacter Player;

    public GameObject UI_StartMenuGO;
    public GameObject UI_BattleGO;
    public GameObject UI_WonGO;
    public GameObject UI_LossGO;

    public GameObject PlayerHealthPanelGO = null;
    public GameObject PlayerManaPanelGO = null;
    public GameObject EnemyHealthPanelGO = null;
    public GameObject EnemyManaPanelGO = null;

    [SerializeField] CharacterRegistry _characterRegistry; 

    //public GameEvent BattleStart;
    //[SerializeField] GameManagerEventChannelSO _gameManagerStartEvent;
    //[SerializeField] GameManagerEventChannelSO _gameManagerBattleEvent;
    //[SerializeField] GameManagerEventChannelSO _gameManagerLossEvent;
    //[SerializeField] GameManagerEventChannelSO _gameManagerWonEvent;

    private void Awake()
    {
        gameManager = GetComponent<GameManager_BS>();
    }

    private void Start()
    {
        Player = _characterRegistry.Player.GetComponent<PlayerCharacter>();
    }

    private void OnEnable()
    {
        //_gameManagerStartEvent.GameManagerEvent += StartUpdateUI;
        //_gameManagerBattleEvent.GameManagerEvent += LoadBattleMenuUI;
        //_gameManagerWonEvent.GameManagerEvent += LoadWinMenuUI;
        //_gameManagerLossEvent.GameManagerEvent += LoadLossMenuUI;
    }

    private void OnDisable()
    {
        //_gameManagerStartEvent.GameManagerEvent -= StartUpdateUI;
        //_gameManagerBattleEvent.GameManagerEvent -= LoadBattleMenuUI;
        //_gameManagerWonEvent.GameManagerEvent -= LoadWinMenuUI;
        //_gameManagerLossEvent.GameManagerEvent -= LoadLossMenuUI;
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadSceneAsync("UI_MainMenu", LoadSceneMode.Additive);
    }

    public void UnloadMainMenu()
    {
        SceneManager.UnloadSceneAsync("UI_MainMenu");
    }

    public void LoadBattleMenuUI()
    {
        SceneManager.LoadSceneAsync("UI_Battle", LoadSceneMode.Additive);
        UnloadMainMenu();
    }

    public void LoadLossMenuUI()
    {        
        SceneManager.UnloadSceneAsync("UI_Battle");
        SceneManager.LoadSceneAsync("UI_LossMenu", LoadSceneMode.Additive);
    }

    public void LoadWinMenuUI()
    {
        SceneManager.UnloadSceneAsync("UI_Battle");
        SceneManager.LoadSceneAsync("UI_WonMenu", LoadSceneMode.Additive);
    }
}
