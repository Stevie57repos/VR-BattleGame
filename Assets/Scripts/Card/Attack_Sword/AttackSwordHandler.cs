using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackSwordHandler : MonoBehaviour, ICardDataTransfer, ICardEffect
{
    [SerializeField] private float _currChargeCount = 0;
    [SerializeField] private float _neededCharge = 3;

    private CardScriptableObject _cardData = null;
    private CardController _cardInfo = null;

    public GameObject StartLine;
    public GameObject EndLine;
    public LineRenderer SwordBeam;

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

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.GetComponent<Enemy_projectile>())
        {
            var enemyProjectile = other.gameObject.GetComponent<Enemy_projectile>();
            _currChargeCount += enemyProjectile._chargeValue;
            _swordMatHandler.SetMaterial(_currChargeCount, _neededCharge);
            enemyProjectile.gameObject.SetActive(false);
        }
        else if(other.gameObject.CompareTag("Ground"))
        {
            _cardEffectEvent.RaiseEvent(_cardInfo.gameObject, _cardData);
            ResetPlayerCardSelection();
            Destroy(this.gameObject);
        }
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
                var enemyController = hit.collider.GetComponent<EnemyCharacter>();
                enemyController.TakeDamage(_cardData.value);
                SwordBeam.endWidth = 2f;
                _cardEffectEvent.RaiseEvent(_cardInfo.gameObject, _cardData);

                ResetPlayerCardSelection();

                Destroy(this.gameObject);
                //GameEventsHub.SwordDamage.CardGO = _cardInfo.gameObject;
                //GameEventsHub.SwordDamage.CardSO = _cardData;
                //Event_SwordDamage.Raise();
            }
        }
    }

    void ResetPlayerCardSelection()
    {
        _cardSelectionEvent.RaiseEvent("None");
    }

    void Update()
    {
        if (_currChargeCount >= _neededCharge)
            DisplaySwordBeam();
    }
}
