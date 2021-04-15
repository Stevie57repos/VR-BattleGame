using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_projectile : MonoBehaviour
{
    // TO DO : make private later and set properties
    public int _chargeValue = 1;
    public Transform targetPos;
    public float speed = 5f;

    private void OnEnable()
    {
        StartCoroutine(MoveToTargetCoroutine(targetPos));
    }

    IEnumerator MoveToTargetCoroutine(Transform target)
    {
        yield return new WaitForSeconds(1f);

        while (Vector3.Distance(transform.position, target.position) > 1f)
        {
            // rotate the sword to face the target
            Vector3 lookAtTarget = target.position - transform.position;
            Quaternion newRotation = Quaternion.LookRotation(lookAtTarget, transform.up);
            //Quaternion targetrotation = new Quaternion(0, newrotation.y, 0, 0);
            transform.rotation = Quaternion.Slerp(transform.rotation, newRotation, Time.fixedDeltaTime);

            //check direction rotation is facing before moving
            float dot = Vector3.Dot(transform.forward, (target.position - transform.position).normalized);
            if (dot > .8f)
            {
                float step = speed * Time.fixedDeltaTime;
                Vector3 targetPos = new Vector3(target.position.x, target.position.y, target.position.z);
                transform.position = Vector3.MoveTowards(transform.position, targetPos, step);
            }
            yield return null;
        }
    }
}
