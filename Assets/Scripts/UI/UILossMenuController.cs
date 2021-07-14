using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UILossMenuController : UIMenuController
{
    private GameManager_BS _gameManager;
    protected override void Awake()
    {
        base.Awake();
        _gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager_BS>();
    }
    public override void SetGameTransitionButtonActivation()
    {
        _gameManager.TransitionToState(_gameManager.BattleState);
    }
}
