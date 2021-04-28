using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class UIBattleMenuController : UIMenuController
{
    [SerializeField] CharacterRegistry _characterRegistry;
    [SerializeField] GameManagerEventChannelSO _gameManagerBattleEvent;

    private PlayerCharacter _playerCharacter;

    [SerializeField] GameManager_BS gameManager;
    [SerializeField] UI_Manager UImanager;

    public GameObject PlayerHealthPanelGO = null;
    public TextMeshProUGUI PlayerHealthPanel = null;
    public GameObject PlayerManaPanelGO = null;
    public TextMeshProUGUI PlayerManaPanel = null;
    
    public GameObject EnemyHealthPanelGO = null;
    public GameObject EnemyManaPanelGO = null;

    private void Start()
    {
        GetTextMesh();
        RegisterUI();
        StartBattle();
    }

    private void GetTextMesh()
    {
        PlayerHealthPanel = PlayerHealthPanelGO.GetComponent<TextMeshProUGUI>();
        PlayerManaPanel = PlayerManaPanelGO.GetComponent<TextMeshProUGUI>();
    }

    private void RegisterUI()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager_BS>();
        UImanager = gameManager.GetComponent<UI_Manager>();
        UImanager.PlayerHealthPanelGO = PlayerHealthPanelGO;
        UImanager.PlayerManaPanelGO = PlayerManaPanelGO;
        UImanager.EnemyHealthPanelGO = EnemyHealthPanelGO;
        UImanager.EnemyManaPanelGO = EnemyManaPanelGO;

        _playerCharacter = _characterRegistry.Player.GetComponent<PlayerCharacter>();
        _playerCharacter.BattleStartUISetup();
    }

    private void StartBattle()
    {
        _gameManagerBattleEvent.RaiseEvent();
    }

    public void UpdatePlayerHealthPanel()
    {
        PlayerHealthPanel.text = _playerCharacter.Health.ToString();
    }

    public void UpdatePlayerManaPanel()
    {
        PlayerManaPanel.text = _playerCharacter.Mana.ToString();
    }



    public override void SetGameTransitionButtonActivation()
    {
        throw new System.NotImplementedException();
    }
}
