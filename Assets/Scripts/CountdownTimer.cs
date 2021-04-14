using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CountdownTimer : MonoBehaviour
{
    float duration = 5f;
    float elapsedTime = 0f;

    private void Update()
    {
        if(elapsedTime == 0)
            elapsedTime -= Time.fixedDeltaTime;
    }

    public bool CountdownTimerCheck(int duration)
    {
        return false;
    }

}
