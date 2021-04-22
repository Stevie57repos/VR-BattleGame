using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class UI_Manager : MonoBehaviour
{
    public GameManager_BS gameManager;
    public PlayerCharacter Player;
    private EnemyManager _enemyManager;

    public GameObject Start_Button;
    public GameObject BattleUIGO;
    public GameObject PlayerHealthPanelGO = null;
    public GameObject PlayerManaPanelGO = null;
    public GameObject EnemyHealthPanelGO = null;
    public GameObject EnemyManaPanelGO = null;

    [SerializeField] CharacterRegistry _characterRegistry; 

    public GameEvent BattleStart;
    [SerializeField] GameManagerEventChannelSO _gameManagerStartEvent;
    [SerializeField] GameManagerEventChannelSO _gameManagerBattleEvent;


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
        //GameManager_BS.Instance.startState.OnGameStart += StartUpdateUI;
        _gameManagerStartEvent.GameManagerEvent += StartUpdateUI;
        _gameManagerBattleEvent.GameManagerEvent += BattleUIStart;

        //GameManager_BS.Instance.battleState.OnBattleStart += BattleUIStart;
    }

    private void OnDisable()
    {
        //GameManager_BS.Instance.startState.OnGameStart -= StartUpdateUI;
        _gameManagerStartEvent.GameManagerEvent -= StartUpdateUI;
        _gameManagerBattleEvent.GameManagerEvent -= BattleUIStart;

        //GameManager_BS.Instance.battleState.OnBattleStart -= BattleUIStart;
    }

    public void StartUpdateUI()
    {
        SetButtonText(Start_Button, "Battle");
        SetButtonActivation(Start_Button);
        BattleUIGO.SetActive(false);
    }

    void SetButtonActivation(GameObject buttonGO)
    {
        Button StartButton = buttonGO.GetComponent<Button>();
        StartButton.onClick.AddListener(TransitionToBattleState);
    }

    public void TransitionToBattleState()
    {
        gameManager.TransitionToState(gameManager.battleState);
    }

    public void BattleUIStart()
    {
        Start_Button.SetActive(false);
        BattleUIGO.SetActive(true);
        BattleStart.Raise();
    }

    public void SetButtonText(GameObject button, string text)
    {
        TextMeshProUGUI buttonTextMesh = button.GetComponentInChildren<TextMeshProUGUI>();
        buttonTextMesh.text = text;
    }
}
