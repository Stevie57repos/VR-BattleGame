using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeDamage : MonoBehaviour
{
    float damageAmount = 50f;

    private void OnCollisionEnter(Collision other)
    {
        ICubeDamage DamageOther = other.gameObject.GetComponent<ICubeDamage>();

        if (DamageOther != null)
            DamageOther.takeDamage(damageAmount);       
     }




}
