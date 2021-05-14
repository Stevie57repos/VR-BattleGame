using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Enemy_projectile : MonoBehaviour
{
    // TO DO : make private later and set properties
    public int _chargeValue = 1;
    public Transform targetPos;
    public float speed = 5f;
    public Rigidbody RBody;

    [Range(0.05f, 5f)]
    public float MaxValueRandomX;
    [Range(-0.05f, -5f)]
    public float MinValueRandomX;
    [Range(0.05f, 2f)]
    public float MaxValueRandomY;

    [Range(0,10)]    
    [SerializeField] private float SelfDestructionTime = 6;

    private CountdownTimer _timer;
    [SerializeField] CharacterRegistry _characterRegistry;

    [SerializeField] SoundsListSO _explosionSounds;
    private AudioSource _audioSource;
    [SerializeField] MeshRenderer _meshRend;
    [SerializeField] SphereCollider _sphereCol;

    private void Awake()
    {
        RBody = GetComponent<Rigidbody>();
        _timer = GetComponent<CountdownTimer>();

        if(_meshRend == null)
            _meshRend = GetComponent<MeshRenderer>();
        _meshRend.enabled = true;
        
        if(_sphereCol == null)       
            _sphereCol = GetComponent<SphereCollider>();
         _sphereCol.enabled = true;

        if (_audioSource == null)
            _audioSource = GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        StopMomentum();
        StartCoroutine(MoveToTargetCoroutine(targetPos));
        StartTimer(SelfDestructionTime);
    }

    void StartTimer(float TimerValue)
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
        float randomX = UnityEngine.Random.Range(MinValueRandomX, MaxValueRandomX);
        float randomY = UnityEngine.Random.Range(0.25f, MaxValueRandomY);
        Vector3 newTargetPos = new Vector3((target.position.x + randomX), (target.position.y + randomY), target.position.z);
        Debug.Log("Target pos is " + target.position);
        Debug.Log("newtargetpos is " + newTargetPos);
        Vector3 direction = newTargetPos - transform.position;
        Vector3 directionNorm = direction.normalized;
        RBody.velocity = directionNorm * speed;
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.GetComponent<PlayerCharacter>())
        {
            _meshRend.enabled = false;
            _sphereCol.enabled = false;
            AudioClip randomClip = _explosionSounds.SoundsArray[UnityEngine.Random.Range(0, _explosionSounds.SoundsArray.Length)];
            _audioSource.PlayOneShot(randomClip);
            other.gameObject.GetComponent<PlayerCharacter>().TakeDamage(_chargeValue);
            Invoke("DisableProjectile", 1.25f);
        }
        else if (other.gameObject.GetComponent<Enemy_projectile>())
        {

        }
        else
        {
            Debug.Log($"projectile destroyed because it touched {other.gameObject.name}");            
            this.gameObject.SetActive(false);
        }
    }

    private void DisableProjectile()
    {
        this.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (_timer.CheckTimer())
        {
            this.gameObject.SetActive(false);
            Debug.Log("Enemy Projectile self destructed");
        }
    }
}
