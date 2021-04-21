using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordMatHandler : MonoBehaviour
{
    public GameObject swordMeshGO;
    public Material[] m_Default = new Material[3];
    public Material[] m_ThirdCharged = new Material[3];
    public Material[] m_MidCharged = new Material[3];
    public Material[] m_FullCharged = new Material[4];

    Attack_Sword_Grab sword;
    Renderer meshRend;

    private void Awake()
    {
        sword = GetComponent<Attack_Sword_Grab>();
        meshRend = swordMeshGO.GetComponent<MeshRenderer>();
    }

    private void Start()
    {
        SetMaterial(0, 1);
    }

    public void SetMaterial(float currentCharge, float neededCharge)
    {
        var currPercentCharge = currentCharge / neededCharge;

        Debug.Log($"percentCharge is currently {currentCharge}");

        if(currPercentCharge == 0)
        {
            Debug.Log($" The currPercentCharge is {currPercentCharge} and material is m_default");
            meshRend.materials = m_Default;
        }
        else if (currPercentCharge >= 0.3f && currPercentCharge <= 0.5f)
        {
            Debug.Log($" The currPercentCharge is {currPercentCharge} and material is m_default");
            meshRend.materials = m_Default;
        }
        else if (currPercentCharge>= 0.5f && currPercentCharge < 1)
        {
            Debug.Log($" The currPercentCharge is {currPercentCharge} and material is m_midcharged");
            meshRend.materials = m_MidCharged;
        }
        else if(currPercentCharge == 1)
        {
            Debug.Log($" The currPercentCharge is {currPercentCharge} and material is full charged");
            meshRend.materials = m_FullCharged;
        }
    }

    


}
