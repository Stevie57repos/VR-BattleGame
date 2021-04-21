using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Attack_Sword_Grab : XRGrabInteractable
{
    [SerializeField] private float _currChargeCount = 0;
    [SerializeField] private float _neededCharge = 3;

    private CardScriptableObject _cardData = null;
    private CardController _cardInfo = null;

    public GameObject StartLine;
    public GameObject EndLine;
    public LineRenderer SwordBeam;

    public GameEvent Event_SwordDamage;

    private Rigidbody RbBody;
    private SwordMatHandler _swordMatHandler;

    // TO DO : force select the sword back into the players hand if its been dropped
    bool inAir = false;

    public void TransferCardData(CardController cardInfo)
    {
        _cardInfo = cardInfo;
        _cardData = cardInfo.CardData;
    }

    protected override void Awake()
    {
        base.Awake();
        SwordBeam = GetComponent<LineRenderer>();
        SwordBeam.enabled = false;
        RbBody = GetComponent<Rigidbody>();
        _swordMatHandler = GetComponent<SwordMatHandler>();
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.GetComponent<Enemy_projectile>())
        {
            var enemyProjectile = other.gameObject.GetComponent<Enemy_projectile>();
            _currChargeCount += enemyProjectile._chargeValue;
            _swordMatHandler.SetMaterial(_currChargeCount, _neededCharge);
            enemyProjectile.gameObject.SetActive(false);
        }
    }
    protected override void OnSelectEntered(XRBaseInteractor interactor)
    {
        base.OnSelectEntered(interactor);
        RbBody.isKinematic = false;
        // 
    }

    protected override void OnSelectExited(XRBaseInteractor interactor)
    {
        base.OnSelectExited(interactor);
        SwordBeam.enabled = false;
    }

    protected override void OnActivate(XRBaseInteractor interactor)
    {
        base.OnActivate(interactor);
        if (_currChargeCount >= _neededCharge)
            FireBeam();
        else
            Debug.Log($" current charge is {_currChargeCount} and you need {_neededCharge}");
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
            Debug.Log($"Firebeam has been activated. Damage valeu is {_cardData.value}");
            if (hit.collider.GetComponent<EnemyCharacter>())
            {
                var enemyController = hit.collider.GetComponent<EnemyCharacter>();
                enemyController.TakeDamage(_cardData.value);
                SwordBeam.endWidth = 2f;
                GameEventsHub.SwordDamage.CardGO = _cardInfo.gameObject;
                GameEventsHub.SwordDamage.CardSO = _cardData;
                Event_SwordDamage.Raise();
                Destroy(this);
            }
        }
    }
    void Update()
    {
        if(_currChargeCount >= _neededCharge)
            DisplaySwordBeam();
    }

}
