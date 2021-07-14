using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StateMachine : MonoBehaviour
{
    protected GameManagerState State;

    public void SetState(GameManagerState state)
    {
        State = state;
    }
}
