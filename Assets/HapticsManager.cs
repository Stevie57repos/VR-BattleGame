using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class HapticsManager : MonoBehaviour
{
    private XRController controller;
    [SerializeField] PlayerSettings _playerSettings;

    public void TriggerHaptics(float hapticStrength, float duration)
    {
        if(controller != null && _playerSettings.isHapticsOn)
        {
            controller.SendHapticImpulse(hapticStrength, duration);
        }
    }

    public void SetController(XRController controller)
    {
        this.controller = controller;
    }

    public void ClearController()
    {
        this.controller = null;
    }

}
