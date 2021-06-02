﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR;

public class CardEffectGrab : XRGrabInteractable
{
    ICardEffect cardEffect;
    [SerializeField] CharacterRegistry _characterRegistry;

    protected override void Awake()
    {
        base.Awake();
        cardEffect = GetComponent<ICardEffect>();

    }

    protected override void OnHoverEntered(XRBaseInteractor interactor)
    {
        base.OnHoverEntered(interactor);
        cardEffect.OnHoverEntered();
    }

    protected override void OnHoverExited(XRBaseInteractor interactor)
    {
        base.OnHoverExited(interactor);
        cardEffect.OnHoverExited();
        
    }

    protected override void OnSelectEntered(XRBaseInteractor interactor)
    {
        base.OnSelectEntered(interactor);
        cardEffect.OnSelectEntered();
        XRController controller = interactor.GetComponent<XRController>();
        cardEffect.PassController(controller);
    }

    protected override void OnSelectExited(XRBaseInteractor interactor)
    {
        base.OnSelectExited(interactor);
        cardEffect.OnSelectExited();
    }

    protected override void OnActivate(XRBaseInteractor interactor)
    {
        base.OnActivate(interactor);
        cardEffect.OnActivate();
    }

    protected override void OnDeactivate(XRBaseInteractor interactor)
    {
        base.OnDeactivate(interactor);
        cardEffect.OnDeactivate();
    }
}


