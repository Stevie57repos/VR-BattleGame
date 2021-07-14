using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoRotate : MonoBehaviour
{
    public float Speed;
    void FixedUpdate()
    {
        transform.Rotate(0f, 0f, Speed);
    }
}
