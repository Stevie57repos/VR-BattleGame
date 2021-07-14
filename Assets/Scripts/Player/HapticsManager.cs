using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class HapticsManager : MonoBehaviour
{
    private XRController _controller;
    [SerializeField] private PlayerSettings _playerSettings;
    public void TriggerHaptics(float hapticStrength, float duration)
    {
        if(_controller != null && _playerSettings.isHapticsOn)
        {
            _controller.SendHapticImpulse(hapticStrength, duration);
        }
    }
    public void SetController(XRController _controller)
    {
        this._controller = _controller;
    }
    public void ClearController()
    {
        this._controller = null;
    }
}
