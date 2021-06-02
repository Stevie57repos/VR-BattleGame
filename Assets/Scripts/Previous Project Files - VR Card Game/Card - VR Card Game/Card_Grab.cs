using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.Events;

public class Card_Grab : XRGrabInteractable
{
    public UnityEvent CollideEvent;
    public bool inAir = false;
    Rigidbody _rigidbody;
    ParticleSystem particleEffects;
    public GameObject cardPlatform;
    MeshRenderer meshCardPlatform;

    protected override void Awake()
    {
        base.Awake();
        _rigidbody = GetComponent<Rigidbody>();
    }

    protected override void OnSelectExited(XRBaseInteractor interactor)
    {
        base.OnSelectExited(interactor);
        _rigidbody.isKinematic = false;
        _rigidbody.useGravity = true;
        inAir = true;
    }

    private void FixedUpdate()
    {

    }

    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.CompareTag("CardPlatform"))
        {
            inAir = false;
            cardPlatform = col.gameObject;
            ChangePlatformColor();
            //Debug.Log("We hit " + col.gameObject.name);
            CollideEvent.Invoke();
        }
        this.GetComponent<CardGameObject>().CardGrab();
    }

    private void ChangePlatformColor()
    {
        meshCardPlatform = cardPlatform.GetComponent<MeshRenderer>();
        meshCardPlatform.material.color = Color.green;
    }
}
