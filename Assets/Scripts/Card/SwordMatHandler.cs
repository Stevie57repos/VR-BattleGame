using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordMatHandler : MonoBehaviour
{
    public GameObject swordMeshGO;
    private Renderer meshRend;
    [Header("Material")]
    public Material[] m_Default = new Material[3];
    public Material[] m_ThirdCharged = new Material[3];
    public Material[] m_MidCharged = new Material[3];
    public Material[] m_FullCharged = new Material[4];
    private void Awake()
    {
        meshRend = swordMeshGO.GetComponent<MeshRenderer>();
    }
    public void SetMaterial(float currentCharge, float neededCharge)
    {
        var currPercentCharge = currentCharge / neededCharge;
        if(currPercentCharge == 0)
        {
            meshRend.materials = m_Default;
        }
        else if (currPercentCharge >= 0.3f && currPercentCharge < 0.5f)
        {
            meshRend.materials = m_Default;
        }
        else if (currPercentCharge >= 0.5f && currPercentCharge < 1)
        {
            meshRend.materials = m_MidCharged;
        }
        else if(currPercentCharge >= 1)
        {
            meshRend.materials = m_FullCharged;
        }
    }
}
