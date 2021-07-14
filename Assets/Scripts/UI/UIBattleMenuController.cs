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
    private EnemyCharacter _enemyCharacter;

    [SerializeField] GameManager_BS gameManager;
    [SerializeField] UI_Manager UImanager;

    public GameObject PlayerHealthPanelGO;
    public TextMeshProUGUI PlayerHealthPanel = null;
    public GameObject PlayerManaPanelGO;
    public TextMeshProUGUI PlayerManaPanel = null;
    
    public GameObject EnemyHealthPanelGO;
    public TextMeshProUGUI EnemyHealthPanel = null;
    public GameObject EnemyManaPanelGO;
    public TextMeshProUGUI EnemyManaPanel = null;
    protected override void Awake()
    {
        base.Awake();
        GetTextMesh();
        RegisterUI();
    }
    private void Start()
    {
        StartBattle();
    }
    private void GetTextMesh()
    {
        PlayerHealthPanel = PlayerHealthPanelGO.GetComponent<TextMeshProUGUI>();
        PlayerManaPanel = PlayerManaPanelGO.GetComponent<TextMeshProUGUI>();
        EnemyHealthPanel = EnemyHealthPanelGO.GetComponent<TextMeshProUGUI>();
        EnemyManaPanel = EnemyManaPanelGO.GetComponent<TextMeshProUGUI>();
    }
    private void RegisterUI()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager_BS>();
        UImanager = gameManager.GetComponent<UI_Manager>();
    }
    private void StartBattle()
    {
        Debug.Log($"start battle has been called");
        _playerCharacter = _characterRegistry.Player.GetComponent<PlayerCharacter>();
        _playerCharacter.BattleStartUISetup();
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
    public void UpdateEnemyHealthPanel()
    {
        CheckForEnemy();
        EnemyHealthPanel = EnemyHealthPanelGO.GetComponent<TextMeshProUGUI>();
        EnemyHealthPanel.text = _enemyCharacter.Health.ToString();
    }
    public void UpdateEnemyManaPanel()
    {
        CheckForEnemy();
        EnemyManaPanel = EnemyManaPanelGO.GetComponent<TextMeshProUGUI>();
        EnemyManaPanel.text = _enemyCharacter.Mana.ToString();
    }
    private void CheckForEnemy()
    {
        if(_enemyCharacter == null)
        {
            _enemyCharacter = _characterRegistry.CurrentEnemy.GetComponent<EnemyCharacter>();
        }
    }
    public override void SetGameTransitionButtonActivation()
    {

    }
    public void DamagePlayer(int damageValue)
    {
        //_playerCharacter.Take5Damage();
        _playerCharacter.TakeDamage(damageValue);
    }
    public void DamageEnemy()
    {
        _enemyCharacter.TakeDamage(5);
    }
}
