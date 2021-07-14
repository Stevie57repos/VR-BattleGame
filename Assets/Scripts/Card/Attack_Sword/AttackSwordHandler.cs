using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EzySlice;
using UnityEngine.XR.Interaction.Toolkit;
public class AttackSwordHandler : MonoBehaviour, ICardDataTransfer, ICardEffect
{
    [Header("Sword Settings")]
    [SerializeField] private float _currChargeCount = 0;
    [SerializeField] private float _neededCharge = 3;
    [SerializeField] Material _dissolveMat;

    [Header("Sword Properties")]
    [SerializeField] Transform _bladeStart;
    [SerializeField] Transform _bladeEnd;
    public Material LaserBlastMaterial;
    public LayerMask sliceableLayer;
    [SerializeField] VelocityEstimator _velocityEstimator;
    [NonSerialized] float _cutVelocityForce = 1000f;

    [Header("Audio")]
    [SerializeField] SoundsListSO _swordSounds;
    [SerializeField] SoundsListSO _projectilerandomDestructionSounds;
    [SerializeField] AudioSource _audioSource;
    [SerializeField] AudioClip _LightningCast;

    [Header("Projectile Aim Assist")]
    public GameObject StartLine;
    public GameObject EndLine;
    public LineRenderer SwordBeam;
    [SerializeField] ParticleSystem _particleSystem;
    public Material TargetingLine;

    [Header("Events")]
    [SerializeField] GameEvent Event_SwordDamage;
    [SerializeField] CardEffectEventChannelSO _cardEffectEvent;
    [SerializeField] CardSelectionEventSO _cardSelectionEvent;

    private Rigidbody _rbBody;
    private SwordMatHandler _swordMatHandler;
    private HapticsManager _hapticsManager;
    private CardScriptableObject _cardData = null;
    private CardController _cardInfo = null;

    public void TransferCardData(CardController cardInfo)
    {
        _cardInfo = cardInfo;
        _cardData = cardInfo.CardData;
    }
    void Awake()
    {
        if(SwordBeam == null)
            SwordBeam = GetComponent<LineRenderer>();
        DisableSwordBeam();
        _rbBody = GetComponent<Rigidbody>();
        _swordMatHandler = GetComponent<SwordMatHandler>();
        _hapticsManager = GetComponent<HapticsManager>();
        PlaySliceSound();
    }
    public void OnHoverEntered()
    {

    }
    public void OnHoverExited()
    {

    }
    public void OnSelectEntered()
    {
        _rbBody.isKinematic = false;
    }
    public void OnSelectExited()
    {
        SwordBeam.enabled = false;
    }
    public void OnActivate()
    {
        if (_currChargeCount >= _neededCharge)
            FireBeam();
        else
            Debug.Log($"Current sword charge is {_currChargeCount} and you need {_neededCharge}");           
    }
    public void OnDeactivate()
    {

    }
    void Update()
    {
        CheckForBladeCutting();
        if (_currChargeCount >= _neededCharge)
        {
            DisplaySwordBeam();
        }
    }
    private void CheckForBladeCutting()
    {
        RaycastHit hit;
        Vector3 bladeDirection = _bladeEnd.position - _bladeStart.position;
        bool hasHit = Physics.Raycast(_bladeStart.position, bladeDirection, out hit, bladeDirection.magnitude, sliceableLayer);
        if (hasHit)
        {
            ChargeSword(hit.transform.gameObject);
            Slice(hit.transform.gameObject, hit.point, _velocityEstimator.GetVelocityEstimate());
        }
    }
    void DisplaySwordBeam()
    {
        Vector3 startPos = StartLine.transform.position;
        Vector3 endPos = EndLine.transform.position;
        Vector3 direction = endPos - startPos;

        SwordBeam.SetPosition(0, startPos);
        if (Physics.Raycast(startPos, direction, out RaycastHit hit))
        {
            if (hit.collider)
            {
                SwordBeam.SetPosition(1, hit.point);
            }
        }
        else
        {
            SwordBeam.SetPosition(1, endPos);
        }
    }
    void FireBeam()
    {
        _hapticsManager.TriggerHaptics(0.5f, 0.3f);
        SwordBeam.material = LaserBlastMaterial;
        SwordBeam.startWidth = 0.75f;
        SwordBeam.endWidth = 2f;
        _audioSource.clip = _LightningCast;
        _audioSource.Play();      

        Vector3 startPos = StartLine.transform.position;
        Vector3 endPos = EndLine.transform.position;
        Vector3 direction = endPos - startPos;

        if (Physics.Raycast(startPos, direction, out RaycastHit hit))
        {
            if (hit.collider.GetComponent<EnemyCharacter>())
            {               
                var enemyController = hit.collider.GetComponent<EnemyCharacter>();
                enemyController.TakeDamage(_cardData.value);
                Reset(1f);
            }
        }
        else
        {
            Debug.Log("didn't hit anything");           
            Reset(1f);
        }
    }
    void DisableSwordBeam()
    {
        SwordBeam.enabled = false;
    }
    void ChargeSword(GameObject enemyProjectileGO)
    {
        var enemyProjectile = enemyProjectileGO.GetComponent<Enemy_projectile>();
        _currChargeCount += enemyProjectile._chargeValue;
        _swordMatHandler.SetMaterial(_currChargeCount, _neededCharge);
        if(_currChargeCount >= _neededCharge)
        {
            SwordBeam.enabled = true;
            SwordBeam.material = TargetingLine;
            SwordBeam.startWidth = 0.25f;
            SwordBeam.endWidth = 0.25f;
            ActivateParticles();
        }
    }
    void ActivateParticles()
    {
        if (_particleSystem == null)
            _particleSystem = GetComponentInChildren<ParticleSystem>();

        _particleSystem.Play();
    }
    private void Slice(GameObject target, Vector3 planePosition, Vector3 bladeVelocity)
    {
        Vector3 bladeCutDirection = _bladeEnd.position - _bladeStart.position;
        Vector3 planeNormal = Vector3.Cross(bladeVelocity, bladeCutDirection);
        
        SlicedHull hull = target.Slice(planePosition, planeNormal);
        if(hull != null)
        {
            GameObject upperHull = hull.CreateUpperHull(target);
            GameObject lowerHull = hull.CreateLowerHull(target);
            CreateSlicedComponents(upperHull);
            CreateSlicedComponents(lowerHull);
            PlaySliceSound();
            PlayRandomDestructionSound();
            _hapticsManager.TriggerHaptics(0.7f, 0.3f);
            target.SetActive(false);
            Destroy(upperHull, 2);
            Destroy(lowerHull, 2);
        }
    }
    private void PlaySliceSound()
    {
        if (_audioSource == null)
            _audioSource = GetComponent<AudioSource>();

        _audioSource.clip = _swordSounds.SoundsArray[UnityEngine.Random.Range(0, _swordSounds.SoundsArray.Length)];
        _audioSource.Play();
    }
    private void PlayRandomDestructionSound()
    {
        if (_audioSource == null)
            _audioSource = GetComponent<AudioSource>();

        var randomDestructionSound = _projectilerandomDestructionSounds.SoundsArray[UnityEngine.Random.Range(0, _projectilerandomDestructionSounds.SoundsArray.Length)];
        _audioSource.PlayOneShot(randomDestructionSound);
    }
    private void CreateSlicedComponents(GameObject slicedHull)
    {
        Rigidbody rb = slicedHull.AddComponent<Rigidbody>();
        MeshCollider collider = slicedHull.AddComponent<MeshCollider>();
        collider.convex = true;
        DissolveV2 dissolveV2 = slicedHull.AddComponent<DissolveV2>();
        MeshRenderer meshRend = slicedHull.GetComponent<MeshRenderer>();
        Material[] tempMat = new Material[] { _dissolveMat};
        meshRend.materials = tempMat;
        dissolveV2.BeginDissolve();

        rb.AddExplosionForce(_cutVelocityForce, slicedHull.transform.position, 1);
    }
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            Reset(0f);
        }
    }
    void Reset(float SecondsBeforeDestruction)
    {
        _cardEffectEvent.RaiseEvent(_cardInfo.gameObject, _cardData);
        _hapticsManager.ClearController();
        Destroy(this.gameObject, SecondsBeforeDestruction);
    }
    public void PassController(XRController controller)
    {
        if (_hapticsManager == null)
            _hapticsManager = GetComponent<HapticsManager>();

        _hapticsManager.SetController(controller);
        _hapticsManager.TriggerHaptics(0.3f, 0.3f);
    }
}
