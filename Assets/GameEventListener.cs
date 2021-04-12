﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Events;

public class GameEventListener : MonoBehaviour
{
    [Tooltip("Event to register with")]
    public GameEvent Event;

    [Tooltip("Response to invoke when Event is raised")]
    public UnityEvent Response;

    public void OnEnable()
    {
        Event.RegisterListener(this);
    }

    public void OnDisable()
    {
        Event.UnregisterListener(this);
    }

    public void OnEventRaised()
    {
        Response.Invoke();
    }
}
