﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class SphereGrab : XRGrabInteractable
{
    public new void SphereOnSelectEntered(XRBaseInteractor interactor)
    {
        base.OnSelectEntered(interactor);
    }
}
