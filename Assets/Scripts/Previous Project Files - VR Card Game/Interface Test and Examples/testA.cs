using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testA : MonoBehaviour
{
    public string boxName;

    public void printName(string name)
    {
        Debug.Log($"Name is {name}");
    }



    private void Update()
    {
        if (boxName != null)
        {
            printName(boxName);
        }  
    }

}
