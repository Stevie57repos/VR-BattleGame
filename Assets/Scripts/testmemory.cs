using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class testObject
{
    public int value;
}

[Serializable]
public struct testStruct 
{
    public int value;
}

public class testmemory : MonoBehaviour
{
    public testStruct Struct;
    public testObject Object;

    private void Update()
    {
        testStructMethod(Struct);
        testStructMethodRef(ref Struct);
        testObjectMethod(Object);
    }

    void testStructMethod(testStruct StructVar)
    {
        StructVar.value++;
    }

    void testStructMethodRef(ref testStruct StructVar)
    {
        StructVar.value++;
    }

    void testObjectMethod(testObject ObjectVar)
    {
        ObjectVar.value++;
    }
}
