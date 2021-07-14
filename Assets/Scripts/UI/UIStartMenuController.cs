using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIStartMenuController : UIMenuController
{
    private GameManager_BS _gameManager;
    public GameObject StartButtonGO;

    protected override void Awake()
    {
        base.Awake();
        SetUpUI();
    }
    public void SetUpUI()
    {
        _gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager_BS>();
        SetGameTransitionButtonActivation();
    }
    public void TransitionToBattleState()
    {
        _gameManager.TransitionToState(_gameManager.battleState);
    }
    public override void SetGameTransitionButtonActivation()
    {
        Button _button = StartButtonGO.GetComponent<Button>();
        _button.onClick.AddListener(TransitionToBattleState);
    }
}
