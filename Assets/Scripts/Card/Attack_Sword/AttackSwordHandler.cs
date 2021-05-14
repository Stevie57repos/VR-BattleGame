using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EzySlice;
using UnityEngine.XR.Interaction.Toolkit;

public class AttackSwordHandler : MonoBehaviour, ICardDataTransfer, ICardEffect
{
    [SerializeField] private float _currChargeCount = 0;
    [SerializeField] private float _neededCharge = 3;

    [SerializeField] SoundsListSO _swordSounds;
    [SerializeField] SoundsListSO _projectilerandomDestructionSounds;
    [SerializeField] AudioSource _audioSource;
    [SerializeField] AudioClip _LightningCast;

    private CardScriptableObject _cardData = null;
    private CardController _cardInfo = null;

    public GameObject StartLine;
    public GameObject EndLine;
    public LineRenderer SwordBeam;
    [SerializeField] ParticleSystem _particleSystem;
    public Material TargetingLine;
    public Material LaserBlastMaterial;

    [SerializeField] Transform _bladeStart;
    [SerializeField] Transform _bladeEnd;
    public LayerMask sliceableLayer;
    [SerializeField] VelocityEstimator _velocityEstimator;
    [NonSerialized] float _cutVelocityForce = 1000f;

    public GameEvent Event_SwordDamage;
    [SerializeField] CardEffectEventChannelSO _cardEffectEvent;
    [SerializeField] CardSelectionEventSO _cardSelectionEvent;

    private Rigidbody RbBody;
    private SwordMatHandler _swordMatHandler;
    private HapticsManager _hapticsManager;

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
        RbBody = GetComponent<Rigidbody>();
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
        RbBody.isKinematic = false;
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
            Debug.Log($"Firebeam has been activated. Damage value is {_cardData.value}");
            if (hit.collider.GetComponent<EnemyCharacter>())
            {               
                var enemyController = hit.collider.GetComponent<EnemyCharacter>();
                enemyController.TakeDamage(_cardData.value);
                //_cardEffectEvent.RaiseEvent(_cardInfo.gameObject, _cardData);
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

    //void ResetPlayerCardSelection(float timeBeforeDestruction)
    //{
    //    _cardSelectionEvent.RaiseEvent("None");
    //    controller = null;
    //    Destroy(this.gameObject, timeBeforeDestruction);
    //}
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
        //GameObject tempSoundGameObject = Instantiate(_tempSoundPrefab, spawnPosition);
        //TempSoundController tempSoundController = tempSoundGameObject.GetComponent<TempSoundController>();
        //var randomDestructionSound = _projectilerandomDestructionSounds.SoundsArray[UnityEngine.Random.Range(0, _projectilerandomDestructionSounds.SoundsArray.Length)];
        //tempSoundController.PlayTempSound(randomDestructionSound, spawnPosition, 100f);
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

        rb.AddExplosionForce(_cutVelocityForce, slicedHull.transform.position, 1);
        Destroy(slicedHull, 2);
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
        //ResetPlayerCardSelection(0f);
        _hapticsManager.ClearController();
        //Destroy(this.gameObject);
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
