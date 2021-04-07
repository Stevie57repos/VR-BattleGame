using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR;

public class SimpeHandAnimator : MonoBehaviour
{
    public XRNode inputSource;


    Animator handAnimator;

    // Start is called before the first frame update
    void Start()
    {
        
    }




    // Update is called once per frame
    void Update()
    {
        updatedHandAnimation();
    }

    void updatedHandAnimation()
    {
        handAnimator = GetComponent<Animator>();

        InputDevice device = InputDevices.GetDeviceAtXRNode(inputSource);
        if(device.TryGetFeatureValue(CommonUsages.trigger, out float triggervalue))
        {
            handAnimator.SetFloat("Trigger", triggervalue);
        }

        if (device.TryGetFeatureValue(CommonUsages.trigger, out float triggerValue))
        {
            handAnimator.SetFloat("Trigger", triggerValue);
        }
        else
        {
            handAnimator.SetFloat("Trigger", 0);
        }

        if (device.TryGetFeatureValue(CommonUsages.grip, out float gripValue))
        {
            handAnimator.SetFloat("Grip", gripValue);
        }
        else
        {
            handAnimator.SetFloat("Grip", 0);
        }
    }
}
