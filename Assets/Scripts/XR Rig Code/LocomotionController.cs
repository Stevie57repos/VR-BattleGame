using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;


public class LocomotionController : MonoBehaviour
{
    public XRController RightTeleportRay;
    public InputHelpers.Button TeleportActivationButton;
    public float ActivationThreshold = 0.1f;
    public XRRayInteractor LeftInteractorRay;
    public XRRayInteractor RightInteractorRay;
    public bool EnabledRightTeleport { get; set; } = true;
    void Update()
    {
        Vector3 pos = new Vector3();
        Vector3 norm = new Vector3();
        int index = 0;
        bool validtarget = false;

        if (RightTeleportRay)
        {
            bool isRightInteractorRayHovering = RightInteractorRay.TryGetHitInfo(out pos, out norm, out index, out validtarget);
            //Debug.Log(isRightInteractorRayHovering);
            RightTeleportRay.gameObject.SetActive(EnabledRightTeleport && CheckIfActivated(RightTeleportRay) && !isRightInteractorRayHovering);
        }              
    }
    public bool CheckIfActivated(XRController controller)
    {
        InputHelpers.IsPressed(controller.inputDevice, TeleportActivationButton, out bool isActivated, ActivationThreshold);
        return isActivated;
    }
}
