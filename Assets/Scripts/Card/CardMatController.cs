using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardMatController : MonoBehaviour, ICardMatGetter
{

    string cardName = null;
    string cardType = null;

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
        //get mesh renderer compononent
        meshRend = GetComponent<MeshRenderer>();
    }
    private void loadDictionary()
    {
        materialDirectory.Add("Attack", Mat_Default);
        materialDirectory.Add("Defend", Mat_Defend);
        materialDirectory.Add("Spell", Mat_Spell);
        materialDirectory.Add("Draw", Mat_Spell);
        materialDirectory.Add("Curse", Mat_Curse);
        materialDirectory.Add("Strength", Mat_Defend);
        materialDirectory.Add("Energy", Mat_Defend);
    }

    public void CardSetUp(string cardName, string cardType)
    {
        this.cardName = cardName;
        this.cardType = cardType;

        SetDefaultMat(cardType);
    }


    public void SetTriggerMaterial()
    {
        Material[] eletricalMatArray = { materialDirectory[cardType], Mat_Electrical };
        meshRend.materials = eletricalMatArray;
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
}









