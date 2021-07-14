using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoRotate : MonoBehaviour
{
    public float speed;
    void FixedUpdate()
    {
        //transform.Rotate(Vector3.z * speed * Time.fixedDeltaTime);
        transform.Rotate(0f, 0f, speed);
        
        //transform.RotateAround(target.transform.position, Vector3.up, 20 * Time.deltaTime);

    }
}
