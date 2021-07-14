using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testD : MonoBehaviour
{
    string Boxname;

    public void takeString(string name)
    {
        Boxname = name;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log($"stevie and {Boxname} make a wonderful team");
    }
}
