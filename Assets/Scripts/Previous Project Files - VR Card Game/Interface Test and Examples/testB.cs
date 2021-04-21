using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testB : MonoBehaviour, box_interface

{
    // Start is called before the first frame update
    string lastName = "Chin";
    string firstName;
    public bool isRun = false;
    bool isComplete = false;

    void printFullName()
    {
        Debug.Log($"{firstName} {lastName}");
    }

    // Update is called once per frame
    void Update()
    {
        if (isRun & !isComplete)
        {
            printFullName();
        }
    }
    public void takeString(string name)
    {
        firstName = name;
    }

}
