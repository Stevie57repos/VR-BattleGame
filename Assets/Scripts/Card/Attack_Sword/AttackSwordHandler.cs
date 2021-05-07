using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EzySlice;

public class AttackSwordHandler : MonoBehaviour, ICardDataTransfer, ICardEffect
{
    [SerializeField] private float _currChargeCount = 0;
    [SerializeField] private float _neededCharge = 3;

    private CardScriptableObject _cardData = null;
    private CardController _cardInfo = null;

    public GameObject StartLine;
    public GameObject EndLine;
    public LineRenderer SwordBeam;

    [SerializeField] Transform _bladeStart;
    [SerializeField] Transform _bladeEnd;
    public LayerMask sliceableLayer;
    [SerializeField] VelocityEstimator _velocityEstimator;
    [NonSerialized] float _cutVelocityForce = 100f;

    public GameEvent Event_SwordDamage;
    [SerializeField] CardEffectEventChannelSO _cardEffectEvent;
    [SerializeField] CardSelectionEventSO _cardSelectionEvent;

    private Rigidbody RbBody;
    private SwordMatHandler _swordMatHandler;

    public void TransferCardData(CardController cardInfo)
    {
        _cardInfo = cardInfo;
        _cardData = cardInfo.CardData;
    }

    void Awake()
    {
        SwordBeam = GetComponent<LineRenderer>();
        SwordBeam.enabled = false;
        RbBody = GetComponent<Rigidbody>();
        _swordMatHandler = GetComponent<SwordMatHandler>();
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

    void DisplaySwordBeam()
    {
        SwordBeam.enabled = true;
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
        SwordBeam.startWidth = .1f;
        SwordBeam.endWidth = .1f;
    }

    void FireBeam()
    {
        Vector3 startPos = StartLine.transform.position;
        Vector3 endPos = EndLine.transform.position;
        Vector3 direction = endPos - startPos;

        if (Physics.Raycast(startPos, direction, out RaycastHit hit))
        {
            Debug.Log($"Firebeam has been activated. Damage value is {_cardData.value}");
            if (hit.collider.GetComponent<EnemyCharacter>())
            {
                Debug.Log("you just fired at an enemy");
                var enemyController = hit.collider.GetComponent<EnemyCharacter>();
                enemyController.TakeDamage(_cardData.value);
                SwordBeam.endWidth = 2f;
                _cardEffectEvent.RaiseEvent(_cardInfo.gameObject, _cardData);

                ResetPlayerCardSelection();
                Destroy(this.gameObject);
            }
            else
            {
                Debug.Log("didn't hit anything");
            }
        }
    }

    void ResetPlayerCardSelection()
    {
        _cardSelectionEvent.RaiseEvent("None");
    }

    void Update()
    {
        RaycastHit hit;
        Vector3 bladeDirection = _bladeEnd.position - _bladeStart.position;
        bool hasHit = Physics.Raycast(_bladeStart.position, bladeDirection, out hit, bladeDirection.magnitude, sliceableLayer);
        if (hasHit)
        {
            ChargeSword(hit.transform.gameObject);
            Slice(hit.transform.gameObject, hit.point, _velocityEstimator.GetVelocityEstimate());
        }
        if (_currChargeCount >= _neededCharge)
            DisplaySwordBeam();
    }

    void ChargeSword(GameObject enemyProjectileGO)
    {
        var enemyProjectile = enemyProjectileGO.GetComponent<Enemy_projectile>();
        _currChargeCount += enemyProjectile._chargeValue;
        _swordMatHandler.SetMaterial(_currChargeCount, _neededCharge);
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

            target.SetActive(false);
        }

    }

    void CreateSlicedComponents(GameObject slicedHull)
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
            Reset();
        }
    }

    void Reset()
    {
        _cardEffectEvent.RaiseEvent(_cardInfo.gameObject, _cardData);
        ResetPlayerCardSelection();
        Destroy(this.gameObject);
    }
}
