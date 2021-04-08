using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(fileName = "GameEvent", menuName = "ScriptableObjects/GameEventSO")]
public class GameEvent : ScriptableObject
{
    public event Action Event;

    public void Raise()
    {
        Event?.Invoke();
    }

}
