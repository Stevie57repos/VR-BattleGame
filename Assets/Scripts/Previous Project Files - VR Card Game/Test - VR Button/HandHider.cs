using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class HandHider : MonoBehaviour
{
    private SkinnedMeshRenderer meshRender = null;
    //private XRDirectInteractor interactor = null;

    private void Awake()
    {
        meshRender = GetComponentInChildren<SkinnedMeshRenderer>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
