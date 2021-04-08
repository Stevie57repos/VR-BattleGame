using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class Timer : MonoBehaviour
{
    [SerializeField] private float duration = 1f;
    [SerializeField] private UnityEvent onTimerEnd = new UnityEvent();

    private void StartTimer() => StartCoroutine(BeginTimer());
    

    private IEnumerator BeginTimer()
    {
        yield return new WaitForSeconds(duration);

        onTimerEnd?.Invoke();
    }

    public void stopTimer()
    {
        StopAllCoroutines();
    }



}

