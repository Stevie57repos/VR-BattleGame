using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class CountdownTimer : MonoBehaviour
{
    public float CurrCowntDownValue;
    public bool isCountDownComplete;
    private void Awake()
    {
        isCountDownComplete = false;
    }
    public void StartTimer(float CountDownValue)
    {
        isCountDownComplete = false;
        StartCoroutine(StartCountDown(CountDownValue));
    }
    private IEnumerator StartCountDown(float countDownValue)
    {
        CurrCowntDownValue = countDownValue;
        while (CurrCowntDownValue > 0)
        {
            yield return new WaitForSeconds(1.0f);
            CurrCowntDownValue--;
        }
        isCountDownComplete = true;
    }
    public bool CheckTimer()
    {
        return isCountDownComplete;
    }
}
