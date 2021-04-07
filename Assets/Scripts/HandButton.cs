using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.Events;

public class HandButton : XRBaseInteractable
{
    public UnityEvent OnPress = null;
    bool previousPress = false;

    private float yMin = 0.0f;
    private float yMax = 0.0f;


    // tracking hand height
    float previousHandHeight = 0.0f;
    public XRBaseInteractor hoverInteractor = null;


    protected override void Awake()
    {
        base.Awake();
        onHoverEntered.AddListener(StartPress);
        onHoverExited.AddListener(EndPress);
        
    }

    protected override void OnDestroy()
    {
        onHoverEntered.RemoveListener(StartPress);
        onHoverExited.RemoveListener(EndPress);
    }
    


    void StartPress(XRBaseInteractor interactor)
    {
        hoverInteractor = interactor;
        previousHandHeight = GetLocalYPosition(hoverInteractor.transform.position);
    }

    void EndPress(XRBaseInteractor interactor)
    {
        hoverInteractor = null;
        previousHandHeight = 0.0f;

        previousPress = false;
        SetYPosition(yMax);
    }

    // Start is called before the first frame update
    void Start()
    {
        SetMinMAx();
    }

    void SetMinMAx()
    {
        Collider col = GetComponent<Collider>();
        yMin = transform.localPosition.y - (col.bounds.size.y * 0.5f);
        yMax = transform.localPosition.y;
    }

    public override void ProcessInteractable(XRInteractionUpdateOrder.UpdatePhase updatePhase)
    {
        if (hoverInteractor)
        {
            float newHandHeight = GetLocalYPosition(hoverInteractor.transform.position);
            float handDifference = previousHandHeight - newHandHeight;
            previousHandHeight = newHandHeight;

            float newPosition = transform.localPosition.y - handDifference;
            SetYPosition(newPosition);

            CheckPress();
        }

    }

    private float GetLocalYPosition(Vector3 position)
    {
        Vector3 localPosition = transform.root.InverseTransformPoint(position);
        return localPosition.y;
    }

    void SetYPosition(float position)
    {
        Vector3 newPosition = transform.localPosition;
        newPosition.y = Mathf.Clamp(position, yMin, yMax);
        transform.localPosition = newPosition;
    }

    void CheckPress()
    {
        bool inPosition = InPosition();
        if (inPosition && inPosition != previousPress)
            OnPress.Invoke();

        previousPress = inPosition;
    }

    bool InPosition()
    {
        float inRange = Mathf.Clamp(transform.localPosition.y, yMin, yMin + 0.01f);
        return transform.localPosition.y == inRange;
    }
}
