using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR;
using System;

public class Box : XRGrabInteractable
{
    // circle prefab
    public GameObject circlePrefab;

    // current hand interactor
    XRBaseInteractor currInteractor = null;

    // variable for tracking the hand interactor
    public XRNode handInteractor;

    // tracking trigger
    bool isTriggerChecking = false;

    protected override void OnSelectEntered(XRBaseInteractor interactor)
    {
        base.OnSelectEntered(interactor);
        currInteractor = interactor;
        isTriggerChecking = true;
        Debug.Log($"On Select Enetered has run and currInteractor is set to {currInteractor.gameObject.name}"); 
    }
    protected override void OnSelectExited(XRBaseInteractor interactor)
    {
        base.OnSelectExited(interactor);
        currInteractor = null;
        isTriggerChecking = false;
        Debug.Log("OnSelectExit has been executed");
    }

    //public void boxDrop(XRBaseInteractor interactor)
    //{
    //    OnSelectExited(interactor);
    //}


    protected override void OnActivate(XRBaseInteractor interactor)
    {
        Debug.Log("trigger button detected");
        //box drop call
        Debug.Log($"{this} is being held by {selectingInteractor}");
        gameObject.SetActive(false);
        CreateAndSelectSphere(interactor);
        //NewXRrayInteractor handInteractor = interactor.GetComponent<NewXRrayInteractor>();
        //handInteractor.ForceDeinteract(this);
       
    }

    private void CreateAndSelectSphere(XRBaseInteractor interactor)
    {
        SphereGrab sphere = CreateSphere();
        SelectSphere(sphere, interactor);
    }

    private SphereGrab CreateSphere()
    {
        GameObject sphereObject = Instantiate(circlePrefab, transform.position, transform.rotation);
        return sphereObject.GetComponent<SphereGrab>();
    }

    private void SelectSphere(SphereGrab sphere, XRBaseInteractor interactor)
    {
        Debug.Log($"Current sphere is {sphere.gameObject.name} and the interactor is {interactor.gameObject.name}");
        Debug.Log($"interaction manager is doing force select");
        interactionManager.ForceSelect(interactor, sphere);

        //sphere.SphereOnSelectEntered(interactor );
        //NewXRrayInteractor handRayInteractor = interactor.GetComponent<NewXRrayInteractor>();
        //handRayInteractor.ForceInteract(sphere);
      
    }



    private void FixedUpdate()
    {
        //if (isTriggerChecking)
        //{
        //    if (CheckTrigger())
        //    {
        //        Debug.Log("Trigger detected");
        //    }
        //}
    }


    //private bool CheckTrigger()
    //{
    //    handInteractor = currInteractor.GetComponent<XRController>().controllerNode;

    //    InputDevice device = InputDevices.GetDeviceAtXRNode(handInteractor);
    //    device.TryGetFeatureValue(CommonUsages.triggerButton, out bool isPressed);
    //    Debug.Log($"the {currInteractor.gameObject.name} trigger button is currently {isPressed}");
    //    return isPressed;
    //}


}
