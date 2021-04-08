using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class MapLevelSocket : XRSocketInteractor
{
    //next map level object and rendner
    public List<GameObject> nextMapLevels = new List<GameObject>();
    MeshRenderer nextMapLevelRenderer;
    public List<GameObject> lineRendList = new List<GameObject>();



    //current map render
    MeshRenderer curMapLevelRenderer;
    [SerializeField] private Material AvailableMat;
    [SerializeField] private Material SelectedMat;


    protected override void Awake()
    {
        base.Awake();
      
        //if(nextMapLevel != null)
        //{
        //    nextMapLevelRenderer = nextMapLevel.GetComponent<MeshRenderer>();
        //}

        curMapLevelRenderer = GetComponent<MeshRenderer>();       
    }

    protected override void OnSelectEntered(XRBaseInteractable interactable)
    {
        base.OnSelectEntered(interactable);
        //change material of current map level selected
        SelectedMaterial();

        // reveal next available levels to select
        AvailableLevel();
    }

    protected override void OnSelectExited(XRBaseInteractable interactable)
    {
        base.OnSelectExited(interactable);

        // turn off line render.
        // turn on the line render
        foreach (GameObject lineRender in lineRendList)
        {
            lineRender.SetActive(false);
        }
    } 



    private void AvailableLevel()
    {
        foreach(GameObject nextLevel in nextMapLevels)
        {
            // change material for next level that are available
            MeshRenderer nextLevelMesh = nextLevel.GetComponent<MeshRenderer>();
            nextLevelMesh.material = AvailableMat;

            // unlock the socket
            MapLevelSocket nextLevelSocket = nextLevel.GetComponent<MapLevelSocket>();
            nextLevelSocket.socketActive = true;
        }

            
        // turn on the line render
        foreach(GameObject lineRender in lineRendList)
        {
            lineRender.SetActive(true);
        }
            
    }

    void SelectedMaterial()
    {
        curMapLevelRenderer.material = SelectedMat;
    }

}
