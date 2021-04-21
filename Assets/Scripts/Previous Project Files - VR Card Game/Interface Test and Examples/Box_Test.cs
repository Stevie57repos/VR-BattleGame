using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box_Test : MonoBehaviour
{
    // string variable
    private string boxName;
    box_interface boxInterface;


    // Start is called before the first frame update
    void Start()
    {
        createName();

        boxInterface = GetComponent<box_interface>();
        boxInterface.takeString(boxName);
    }

    void createName()
    {
        boxName = "Sam";
    }






}
