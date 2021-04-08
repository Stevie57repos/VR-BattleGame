using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class ColorChanger : MonoBehaviour
{
    public Material selectMaterial = null;

    MeshRenderer meshRenderer = null;
    XRBaseInteractable interactable = null;
    Material originalMaterial = null;

    private void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        originalMaterial = meshRenderer.material;

        interactable = GetComponent<XRBaseInteractable>();
        interactable.onHoverEntered.AddListener(SetSelectMaterial);
        interactable.onHoverExited.AddListener(SetOriginalMaterial);
    }

    private void SetOriginalMaterial(XRBaseInteractor interactor)
    {
        meshRenderer.material = selectMaterial;
    }

    private void SetSelectMaterial(XRBaseInteractor interactor)
    {
        meshRenderer.material = originalMaterial;
    }
}
