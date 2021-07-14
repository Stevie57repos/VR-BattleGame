using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class UIMenuController : MonoBehaviour
{
    private Canvas _canvas;
    private Canvas[] _canvasList;
    protected virtual void Awake()
    {
        SetupCamaera();
    }
    public void SetupCamaera()
    {
        //_canvas = GetComponent<Canvas>();
        //_canvas.worldCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        _canvasList = GetComponentsInChildren<Canvas>();
        var mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        foreach (Canvas _canvas in _canvasList)
        {
            _canvas.worldCamera = mainCamera;
        }
    }
    public abstract void SetGameTransitionButtonActivation();
    public void QuitGame()
    {
        Application.Quit();
    }
}
