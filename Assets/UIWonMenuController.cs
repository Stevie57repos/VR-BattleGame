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

    void Start()
    {
        
    }

    public override void SetGameTransitionButtonActivation()
    {
        _gameManager.TransitionToState(_gameManager.battleState);
    }

    // UI
    // GameManager // battle start event
    // Enemy Manager
    // Player deck reload
}
