using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExampleScript : MonoBehaviour
{
    public bool isGameManager = false;
    GameManager_BS gamemanager = null;

    private void Awake()
    {
        gamemanager = GetComponent<GameManager_BS>();
    }

    void Start()
    {
        CheckGameManager();
    }

    void CheckGameManager()
    {
        if (gamemanager == null)
            Debug.Log("game manager is null");
        else
            isGameManager = true;        
    }

}
