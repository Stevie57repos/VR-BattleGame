using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionOnTimer : MonoBehaviour
{
    private float timer;

    public void SetTimer(float timer)
    {
        this.timer = timer;
    }

    private void Update ()
    {
        timer -= Time.deltaTime;
    }

    public bool IsTimerComplete()
    {
        return timer <= 0f;
    }


}
