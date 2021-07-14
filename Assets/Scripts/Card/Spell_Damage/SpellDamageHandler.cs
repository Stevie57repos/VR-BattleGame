using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class SpellDamageHandler : MonoBehaviour, ICardEffect, ICardDataTransfer
{
    [Header("SpellDamage Settings")]
    [SerializeField] private int _maxAdditionalCharge = 1;
    [SerializeField] private float _speed;
    private int _currentSpellDamage;
    private int _maxCharge;
    [SerializeField] private CharacterRegistry _characterRegistry;
    public Vector3 TargetPos;
    private GameObject _targetGO;
    [SerializeField] private SphereCollider _sphereCol;
    [SerializeField] private Rigidbody _rBody;

    [Header("Audio")]
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip _idleSpell;
    [SerializeField] private SoundsListSO _castSpell;
    [SerializeField] private AudioClip _chargeSpell;

    [Header("Events")]
    [SerializeField] private CardEffectEventChannelSO _cardEffectEvent;
    [SerializeField] private CardSelectionEventSO _cardSelectionEvent;

    private HapticsManager _hapticsManager;
    private CardScriptableObject _cardData = null;
    private CardController _cardInfo = null;
    private void Awake()
    {
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
        _maxCharge = _cardData.value + _maxAdditionalCharge;
    }
    public void OnHoverEntered()
    {
    }

    public void OnHoverExited()
    {
    }

    public void OnSelectEntered()
    {
    }

    public void OnSelectExited()
    {
        _audioSource.Stop();
        _audioSource.loop = false;
        AudioClip randomClip = _castSpell.SoundsArray[UnityEngine.Random.Range(0, _castSpell.SoundsArray.Length)];
        _audioSource.PlayOneShot(randomClip);
        _targetGO = _characterRegistry.CurrentEnemy;
        StartCoroutine(MoveToTargetCoroutine(_targetGO));
        Timer timer = GetComponent<Timer>();
        timer.StartTimer(10f);
    }
    public void OnActivate()
    {
    }
    public void OnDeactivate()
    {
    }
    Vector3 FindTargetPos(GameObject _targetGO)
    {
        if(_targetGO == null)
            _targetGO = _characterRegistry.CurrentEnemy;
        Transform TargetTransform = _targetGO.transform;
        Debug.Log("findtarget pos has been called");
        return TargetTransform.position;
    }
    IEnumerator MoveToTargetCoroutine(GameObject _targetGO)
    {
        yield return new WaitForSeconds(0.5f);
        var target = FindTargetPos(_targetGO);
        while (Vector3.Distance(transform.position, target) > 0.5f)
        {
            Debug.Log("moving towards target");
            Vector3 lookAtTarget = target - transform.position;
            Quaternion newRotation = Quaternion.LookRotation(lookAtTarget, transform.up);
            transform.rotation = Quaternion.Slerp(transform.rotation, newRotation, Time.fixedDeltaTime);

            float step = _speed * Time.fixedDeltaTime;
            Vector3 TargetPos = FindTargetPos(_targetGO);
            Vector3 updatedPos = new Vector3(TargetPos.x, TargetPos.y + 1f, TargetPos.z);
            transform.position = Vector3.MoveTowards(transform.position, updatedPos, step);
            yield return null;
        }
        if (Vector3.Distance(transform.position, target) < 0.5f)
        {
            Debug.Log("distance has been reached");
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
        if(_currentSpellDamage > _maxCharge)      
            _currentSpellDamage = _maxCharge;       
    }
    public void SpellProjectileDestruction()
    {
        StopAllCoroutines();
        StopMomentum();
        SpellDamageEvent(_cardInfo.gameObject, _cardData);
        ResetPlayerCardSelection();
        Destroy(this.gameObject);
    }
    public void SpellProjectileFailDestruction()
    {
        StopAllCoroutines();
        StopMomentum();
        ResetPlayerCardSelection();
        Destroy(this.gameObject);
    }
    private void StopMomentum()
    {
        if (_rBody == null)
            _rBody = GetComponent<Rigidbody>();
        _rBody.velocity = Vector3.zero;
        _rBody.angularVelocity = Vector3.zero;
    }
    public void SpellDamageEvent(GameObject cardObject, CardScriptableObject cardData)
    {
        EnemyCharacter enenyCharacter = _characterRegistry.CurrentEnemy.GetComponent<EnemyCharacter>();
        enenyCharacter.TakeDamage(_currentSpellDamage);
        _cardEffectEvent.RaiseEvent(cardObject, cardData);
    }
    private void ResetPlayerCardSelection()
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
