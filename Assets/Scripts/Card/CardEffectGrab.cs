using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR;

public class CardEffectGrab : XRGrabInteractable
{
    private ICardEffect _cardEffect;
    [SerializeField] CharacterRegistry _characterRegistry;
    protected override void Awake()
    {
        base.Awake();
        _cardEffect = GetComponent<ICardEffect>();
    }
    protected override void OnHoverEntered(XRBaseInteractor interactor)
    {
        base.OnHoverEntered(interactor);
        _cardEffect.OnHoverEntered();
    }
    protected override void OnHoverExited(XRBaseInteractor interactor)
    {
        base.OnHoverExited(interactor);
        _cardEffect.OnHoverExited();
    }
    protected override void OnSelectEntered(XRBaseInteractor interactor)
    {
        base.OnSelectEntered(interactor);
        _cardEffect.OnSelectEntered();
        XRController controller = interactor.GetComponent<XRController>();
        _cardEffect.PassController(controller);
    }
    protected override void OnSelectExited(XRBaseInteractor interactor)
    {
        base.OnSelectExited(interactor);
        _cardEffect.OnSelectExited();
    }
    protected override void OnActivate(XRBaseInteractor interactor)
    {
        base.OnActivate(interactor);
        _cardEffect.OnActivate();
    }
    protected override void OnDeactivate(XRBaseInteractor interactor)
    {
        base.OnDeactivate(interactor);
        _cardEffect.OnDeactivate();
    }
}


