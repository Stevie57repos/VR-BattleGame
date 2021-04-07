using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testC : MonoBehaviour, box_interface
{
    string boxName;
    public Material blackM;

    public void takeString(string name)
    {
        boxName = name;
    }

    private void Update()
    {
        if (checkString())
        {
            changeMaterial();
        }
    }

    private bool checkString()
    {
        if(boxName == "Sam")
        {
            return true;
        }
        return false;
    }

    private void changeMaterial()
    {
        var meshRenderer = GetComponent<MeshRenderer>();
        meshRenderer.material = blackM;
    }
}
