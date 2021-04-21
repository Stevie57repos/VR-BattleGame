using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class VoidEventChannelSO : ScriptableObject
{
    public UnityAction OnGameManagerStart;
    public void RaiseEvent()
    {
        if (OnGameManagerStart != null)
        {
            OnGameManagerStart.Invoke();
        }
        else
        {
            Debug.LogWarning("ON Game Manager Start was requested but nobody picked it up");
        }
    }
}
