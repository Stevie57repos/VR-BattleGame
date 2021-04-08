using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Text_Insert : MonoBehaviour
{

    TextMeshProUGUI TextDisplay;


    // Start is called before the first frame update
    void Start()
    {
        TextDisplay = GetComponent<TextMeshProUGUI>();
        InsertText();
    }

    private void InsertText()
    {
        TextDisplay.text = "New Text";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
