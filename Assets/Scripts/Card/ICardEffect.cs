using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICardEffect 
{
    void OnHoverEntered();

    void OnHoverExited();

    void OnSelectEntered();

    void OnSelectExited();

    void OnActivate();

    void OnDeactivate();
}
