using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class ContinousMovement : MonoBehaviour
{
    public float Speed = 1f;
    public XRNode InputSource;
    public float Gravity = -9.81f;
    public LayerMask GroundLayer;
    public float AdditionalHeight = 0.2f;
    private float FallingSpeed;
    private XRRig Rig;
    private Vector2 InputAxis;
    private CharacterController Character;
 
    void Start()
    {
        Character = GetComponent<CharacterController>();
        Rig = GetComponent<XRRig>();
    }

    void Update()
    {
        InputDevice device = InputDevices.GetDeviceAtXRNode(InputSource);
        device.TryGetFeatureValue(CommonUsages.primary2DAxis, out InputAxis);
    }
    private void FixedUpdate()
    {
        CapsuleFollowHeadset();
        Quaternion headYaw = Quaternion.Euler(0, Rig.cameraGameObject.transform.eulerAngles.y, 0);
        Vector3 direction = headYaw * new Vector3(InputAxis.x, 0, InputAxis.y);

        Character.Move(direction * Time.fixedDeltaTime * Speed );

        bool isGrounded = checkIfGrounded();
        if (isGrounded)
            FallingSpeed = 0;
        else
            FallingSpeed += Gravity * Time.fixedDeltaTime;

        Character.Move(Vector3.up * FallingSpeed * Time.fixedDeltaTime);
    }

    bool checkIfGrounded()
    {
        Vector3 rayStart = transform.TransformPoint(Character.center);

        float rayLength = Character.center.y + 0.01f;
        bool hasHit = Physics.SphereCast(rayStart, Character.radius, Vector3.down, out RaycastHit hitInfo, rayLength, GroundLayer);
        return hasHit;
    }
    void CapsuleFollowHeadset()
    {
        Character.height = Rig.cameraInRigSpaceHeight + AdditionalHeight;
        Vector3 capsuleCenter = transform.InverseTransformPoint(Rig.cameraGameObject.transform.position);
        Character.center = new Vector3(capsuleCenter.x, Character.height / 2 + Character.skinWidth, capsuleCenter.z);
    }
}
