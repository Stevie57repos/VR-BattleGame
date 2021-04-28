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
    [SerializeField] GameManagerEventChannelSO _gameManagerStartEvent;
    [SerializeField] GameManagerEventChannelSO _gameManagerBattleEvent;
    [SerializeField] GameManagerEventChannelSO _gameManagerLossEvent;
    [SerializeField] GameManagerEventChannelSO _gameManagerWonEvent;

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
        _gameManagerWonEvent.GameManagerEvent += WonUI;
        _gameManagerLossEvent.GameManagerEvent += LossUI;
    }

    private void OnDisable()
    {
        //_gameManagerStartEvent.GameManagerEvent -= StartUpdateUI;
        //_gameManagerBattleEvent.GameManagerEvent -= LoadBattleMenuUI;
        _gameManagerWonEvent.GameManagerEvent -= WonUI;
        _gameManagerLossEvent.GameManagerEvent -= LossUI;
    }

    public void StartUpdateUI()
    {
        UI_StartMenuGO.SetActive(true);
        UI_BattleGO.SetActive(false);
        UI_WonGO.SetActive(false);
        UI_LossGO.SetActive(false);
    }



    //public void BattleUI()
    //{
    //    UI_BattleGO.SetActive(true);
    //    //BattleStart.Raise();
    //    UI_WonGO.SetActive(false);
    //    UI_LossGO.SetActive(false);
    //}

    private void WonUI()
    {
        UI_StartMenuGO.SetActive(false);
        UI_BattleGO.SetActive(false);
        UI_WonGO.SetActive(true);
        UI_LossGO.SetActive(false);
    }
    private void LossUI()
    {
        //UI_StartMenuGO.SetActive(false);
        //UI_BattleGO.SetActive(false);
        //UI_WonGO.SetActive(false);
        //UI_LossGO.SetActive(true);
        Debug.Log("player loss");
    }

    void SetButtonActivation(GameObject buttonGO)
    {
        Button StartButton = buttonGO.GetComponentInChildren<Button>();
        StartButton.onClick.AddListener(TransitionToBattleState);
    }

    public void SetButtonText(GameObject button, string text)
    {
        TextMeshProUGUI buttonTextMesh = button.GetComponentInChildren<TextMeshProUGUI>();
        buttonTextMesh.text = text;
    }

    public void TransitionToBattleState()
    {
        gameManager.TransitionToState(gameManager.battleState);
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

}
