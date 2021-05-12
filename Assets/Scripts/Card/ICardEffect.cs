using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public interface ICardEffect 
{
    void PassController(XRController controller);

    void OnHoverEntered();

    void OnHoverExited();

    void OnSelectEntered();

    void OnSelectExited();

    void OnActivate();

    void OnDeactivate();
}
