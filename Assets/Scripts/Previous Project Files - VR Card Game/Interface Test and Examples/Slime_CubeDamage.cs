using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime_CubeDamage : MonoBehaviour
{
    float health = 100;

    public void dealDamage(float damage)
    {
        
    }

    public void takeDamage(float damage)
    {
        health -= damage;
    }

    //Debug.Log($"Slime has takken {damage}");

    //health -= damage;

    //if (health <= 0)
    //{
    //    Destroy(this);
    //}



}
