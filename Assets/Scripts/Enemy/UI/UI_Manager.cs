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

    private void Awake()
    {
        gameManager = GetComponent<GameManager_BS>();
    }

    private void Start()
    {
        Player = _characterRegistry.Player.GetComponent<PlayerCharacter>();
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
