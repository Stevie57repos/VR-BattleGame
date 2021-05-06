using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_projectile : MonoBehaviour
{
    // TO DO : make private later and set properties
    public int _chargeValue = 1;
    public Transform targetPos;
    public float speed = 5f;
    public Rigidbody RBody;

    private CountdownTimer _timer;

    [SerializeField] CharacterRegistry _characterRegistry;

    private void Awake()
    {
        RBody = GetComponent<Rigidbody>();
        _timer = GetComponent<CountdownTimer>();
    }

    private void OnEnable()
    {
        StopMomentum();
        StartCoroutine(MoveToTargetCoroutine(targetPos));
        StartTimer(4);
    }

    void StartTimer(int TimerValue)
    {
        _timer.StartTimer(TimerValue);
    }

    private void StopMomentum()
    {
        RBody.velocity = Vector3.zero;
        RBody.angularVelocity = Vector3.zero;
    }

    IEnumerator MoveToTargetCoroutine(Transform target)
    {
        yield return new WaitForSeconds(1f);
        while (Vector3.Distance(transform.position, target.position) > 10f)
        {
            // rotate the sword to face the target
            Vector3 lookAtTarget = target.position - transform.position;
            Quaternion newRotation = Quaternion.LookRotation(lookAtTarget, transform.up);
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
        FlyTowardsPlayer(target);
    }

    private void FlyTowardsPlayer(Transform target)
    {
        Vector3 direction = target.position - transform.position;
        //Vector3 finalTarget = (direction.x, (direction.y + 1), direction.z);
        RBody.velocity = direction * (speed/4);
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.GetComponent<PlayerCharacter>())
        {
            other.gameObject.GetComponent<PlayerCharacter>().TakeDamage(_chargeValue);
            this.gameObject.SetActive(false);
        }
        else if (other.gameObject.GetComponent<Enemy_projectile>())
        {

        }
        else
        {
            this.gameObject.SetActive(false);
        }
    }

    private void Update()
    {
        if (_timer.CheckTimer())
            this.gameObject.SetActive(false);
    }
}
