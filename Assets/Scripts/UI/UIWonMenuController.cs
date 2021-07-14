using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIWonMenuController : UIMenuController
{
    private GameManager_BS _gameManager;
    public GameObject NextLevelGO;
    public GameObject MainMenuGO;
    public GameObject QuitGO;
    protected override void Awake()
    {
        base.Awake();
        _gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager_BS>();
    }
    public override void SetGameTransitionButtonActivation()
    {
        EnemyManager enemyManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<EnemyManager>();
        enemyManager.currentLevel++;
        _gameManager.TransitionToState(_gameManager.BattleState);
    }
}
