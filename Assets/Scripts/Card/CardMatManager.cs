using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardMatManager : MonoBehaviour, ICardMatGetter
{

    // Variables for material change when the trigger button is activated
    private Renderer meshRend;

    // reference for which material to use
    Dictionary<string, Material> materialDirectory = new Dictionary<string, Material>();

    // default is the attack skin
    [SerializeField] private Material Mat_Default;
    [SerializeField] private Material Mat_Defend;
    [SerializeField] private Material Mat_Spell;
    [SerializeField] private Material Mat_Curse;

    //electrical material overlay
    [SerializeField] private Material Mat_Electrical;

    // Start is called before the first frame update
    void Awake()
    {
        loadDictionary();
    }
    private void loadDictionary()
    {
        Debug.Log($"CardMatManager dictionary has been loaded");
        materialDirectory.Add("Attack", Mat_Default);
        materialDirectory.Add("Defend", Mat_Defend);
        materialDirectory.Add("Spell", Mat_Spell);
        materialDirectory.Add("Draw", Mat_Spell);
        materialDirectory.Add("Curse", Mat_Curse);
        materialDirectory.Add("Strength", Mat_Defend);
        materialDirectory.Add("Energy", Mat_Defend);
    }

    void Start()
    {
        //get mesh renderer compononent
        meshRend = GetComponent<MeshRenderer>();
    }

    public void SetDefaultMat(string cardtype)
    {
        if (meshRend == null)
        {
            meshRend = GetComponent<MeshRenderer>();
        }

        Material[] defaultmat = { materialDirectory[cardtype] };
        meshRend.materials = defaultmat;
    }

    public void SetTriggerMaterial(string cardtype)
    {
        Material[] eletricalMatArray = { materialDirectory[cardtype], Mat_Electrical };
        meshRend.materials = eletricalMatArray;
    }
}









