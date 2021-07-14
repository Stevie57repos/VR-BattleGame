using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class TimelineManager : MonoBehaviour
{
    public PlayableDirector cubePlayableTest;
    [SerializeField] GameManagerEventChannelSO battleStart;

    private void OnEnable()
    {
        battleStart.GameManagerEvent += playCube;
    }

    private void OnDisable()
    {
        battleStart.GameManagerEvent -= playCube;
    }

    void playCube()
    {
        cubePlayableTest.Play();
    }

    public void SignalTest()
    {
        Debug.Log("Cube Signal Received");
    }
}
