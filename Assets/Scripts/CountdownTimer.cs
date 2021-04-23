using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CountdownTimer : MonoBehaviour
{

    public float currCowntDownValue;
    public bool isCountDownComplete;

    private void Awake()
    {
        isCountDownComplete = false;
    }

    private IEnumerator StartCountDown(float countDownValue)
    {
        currCowntDownValue = countDownValue;
        while(currCowntDownValue > 0)
        {
            yield return new WaitForSeconds(1.0f);
            currCowntDownValue--;
        }
        isCountDownComplete = true;
    }

    public void StartTimer(int CountDownValue)
    {
        isCountDownComplete = false;
        StartCoroutine(StartCountDown(CountDownValue));
    }

    public bool CheckTimer()
    {
        return isCountDownComplete;
    }
}
