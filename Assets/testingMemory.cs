using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Profiling;

public class testingMemory : MonoBehaviour
{
    WaitForSeconds testingWaitforseconds = new WaitForSeconds(1);

    // passing by reference
    // passing by value 

    void Start()
    {
        
    }


    void Update()
    {
        Profiler.BeginSample("TestingStatic");
        testingStatic();
        Profiler.EndSample();

        Profiler.BeginSample("TestingNew");       
        testingNew();        
        Profiler.EndSample();
    }

    void testingStatic()
    {
        WaitForSeconds test2 = testingWaitforseconds;
    }

    void testingNew()
    {
        WaitForSeconds test3 = new WaitForSeconds(1);
    }

}
