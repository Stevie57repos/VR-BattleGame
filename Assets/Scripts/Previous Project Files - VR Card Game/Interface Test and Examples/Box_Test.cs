using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box_Test : MonoBehaviour
{
    // string variable
    private string boxName;
    testA Output;


    // Start is called before the first frame update
    void Start()
    {
        createName();

        Output = GetComponent<testA>();
        Output.boxName = this.boxName;

    }

    void createName()
    {
        boxName = "Sam";
    }







    //box_interface boxInterface;
    //boxInterface = GetComponent<box_interface>();
    //boxInterface.takeString(boxName);


}
