using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class SpellDamageHandler : MonoBehaviour, ICardEffect, ICardDataTransfer
{
    private CardScriptableObject _cardData = null;
    private CardController _cardInfo = null;
    
    [SerializeField] HapticsManager _hapticsManager;

    [SerializeField] AudioSource _audioSource;
    [SerializeField] AudioClip _idleSpell;
    [SerializeField] SoundsListSO _castSpell;
    [SerializeField] AudioClip _chargeSpell;

    bool inAir = false;
    public Vector3 targetPos;
    private GameObject targetGO;
    [SerializeField] private float speed;
    [SerializeField] CardEffectEventChannelSO _cardEffectEvent;
    [SerializeField] CharacterRegistry _characterRegistry;
    [SerializeField] CardSelectionEventSO _cardSelectionEvent;

    [SerializeField] SphereCollider _sphereCol;
    [SerializeField] Rigidbody _rBody;
    private int _currentSpellDamage;
    [SerializeField] int maxAdditionalCharge = 1;
    private int maxCharge;

    private void Awake()
    {
        inAir = false;
        if(_hapticsManager == null)
            _hapticsManager = GetComponent<HapticsManager>();

        if (_audioSource == null)
            _audioSource = GetComponent<AudioSource>();

        _audioSource.clip = _idleSpell;
        _audioSource.loop = true;
        _audioSource.Play();
    }

    public void TransferCardData(CardController cardInfo)
    {
        _cardInfo = cardInfo;
        _cardData = cardInfo.CardData;
        _currentSpellDamage = _cardData.value;
        maxCharge = _cardData.value + maxAdditionalCharge;
    }

    public void OnHoverEntered()
    {

    }

    public void OnHoverExited()
    {

    }

    public void OnSelectEntered()
    {
        inAir = false;
    }

    public void OnSelectExited()
    {
        inAir = true;
        _audioSource.Stop();
        _audioSource.loop = false;
        AudioClip randomClip = _castSpell.SoundsArray[UnityEngine.Random.Range(0, _castSpell.SoundsArray.Length)];
        _audioSource.PlayOneShot(randomClip);
        targetGO = _characterRegistry.CurrentEnemy;
        StartCoroutine(MoveToTargetCoroutine(targetGO));
    }
    public void OnActivate()
    {

    }

    public void OnDeactivate()
    {

    }

    private void FixedUpdate()
    {
        if (inAir)
        {
            targetPos = FindTargetPos(targetGO);
            //CheckDistance(targetGO);
        }
    }
    Vector3 FindTargetPos(GameObject targetGO)
    {
        Transform TargetTransform = targetGO.transform;
        Debug.Log("findtarget pos has been called");
        return TargetTransform.position;
    }
    IEnumerator MoveToTargetCoroutine(GameObject targetGO)
    {
        yield return new WaitForSeconds(1.5f);

        var target = targetPos;

        while (Vector3.Distance(transform.position, target) > 0.5f)
        {
            Vector3 lookAtTarget = target - transform.position;
            Quaternion newRotation = Quaternion.LookRotation(lookAtTarget, transform.up);
            transform.rotation = Quaternion.Slerp(transform.rotation, newRotation, Time.fixedDeltaTime);

            float dot = Vector3.Dot(transform.forward, (target - transform.position).normalized);
            if (dot > .8f)
            {
                float step = speed * Time.fixedDeltaTime;
                Vector3 targetPos = new Vector3(target.x, target.y, target.z);
                transform.position = Vector3.MoveTowards(transform.position, targetPos, step);
            }
            yield return null;
        }

        if (Vector3.Distance(transform.position, target) < 0.5f)
        {
            Debug.Log("distance has been reached");
        }
    }

    void CheckDistance(GameObject target)
    {
        if (Vector3.Distance(transform.position, target.transform.position) < 2.5f && target != null)
        {
            Debug.Log("distance check execution");
            SpellProjectileDestruction();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("collision detected with " + collision.gameObject.name);
        if (collision.gameObject.GetComponent<EnemyCharacter>())
        {
            SpellProjectileDestruction();
        }
        else if (collision.gameObject.GetComponent<Enemy_projectile>()) 
        {
            _audioSource.PlayOneShot(_chargeSpell);
            ChargeSpell();
            collision.gameObject.SetActive(false);
        }
    }

    private void ChargeSpell()
    {
        _currentSpellDamage++;
        if(_currentSpellDamage > maxCharge)      
            _currentSpellDamage = maxCharge;       
    }

    void SpellProjectileDestruction()
    {
        StopAllCoroutines();
        StopMomentum();
        SpellDamageEvent(_cardInfo.gameObject, _cardData);
        ResetPlayerCardSelection();
        Destroy(this.gameObject);
        //Destroy(this.gameObject, 0.5f);
    }

    void ProjectileTimeOut()
    {
        if (this.enabled == true)
        {
            //ResetPlayerCardSelection();
            //Debug.Log("timed out");
            //Destroy(this.gameObject);
        }
    }

    private void StopMomentum()
    {
        if (_rBody == null)
            _rBody = GetComponent<Rigidbody>();
        _rBody.velocity = Vector3.zero;
        _rBody.angularVelocity = Vector3.zero;
    }
    void SpellDamageEvent(GameObject cardObject, CardScriptableObject cardData)
    {
        EnemyCharacter enenyCharacter = _characterRegistry.CurrentEnemy.GetComponent<EnemyCharacter>();
        enenyCharacter.TakeDamage(_currentSpellDamage);
        _cardEffectEvent.RaiseEvent(cardObject, cardData);
    }
    void ResetPlayerCardSelection()
    {
        _cardSelectionEvent.RaiseEvent("None");
    }
    public void PassController(XRController controller)
    {
        if (_hapticsManager == null)
            _hapticsManager = GetComponent<HapticsManager>();

        _hapticsManager.SetController(controller);
        _hapticsManager.TriggerHaptics(0.3f, 0.3f);
    }
}
