using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class NewXRrayInteractor : XRRayInteractor
{
    public void ForceInteract(XRBaseInteractable interactable)
    {
        OnSelectEntered(interactable);
    }
    public void ForceDeinteract(XRBaseInteractable interactable)
    {
        OnSelectExited(interactable);
    }
}
