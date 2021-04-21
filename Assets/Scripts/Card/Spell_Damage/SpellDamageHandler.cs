using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellDamageHandler : MonoBehaviour, ICardEffect, ICardDataTransfer
{
    private CardScriptableObject _cardData = null;
    private CardController _cardInfo = null;

    bool inAir = false;
    public Transform targetPos;
    [SerializeField] private float speed;
    [SerializeField] CardEffectEventChannelSO _cardEffectEvent;
    private Rigidbody _rigidBody;

    public GameEvent Event_SpellDamage;

    private void Awake()
    {
        targetPos = GameManager_BS.Instance.enemyManager.currentEnemyGO.transform;
        _rigidBody = GetComponent<Rigidbody>();
        inAir = false;
        StopCoroutine(MoveToTargetCoroutine(targetPos));
    }

    public void TransferCardData(CardController cardInfo)
    {
        _cardInfo = cardInfo;
        _cardData = cardInfo.CardData;
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
        if (_rigidBody.useGravity == false)
            _rigidBody.useGravity = false;
    }
    public void OnActivate()
    {

    }

    public void OnDeactivate()
    {

    }

    private void Update()
    {
        if (inAir)
        {
            StartCoroutine(MoveToTargetCoroutine(targetPos));
        }
    }

    IEnumerator MoveToTargetCoroutine(Transform target)
    {
        yield return new WaitForSeconds(1.5f);

        while (Vector3.Distance(transform.position, target.position) > 1f)
        {

            Vector3 lookAtTarget = target.position - transform.position;
            Quaternion newRotation = Quaternion.LookRotation(lookAtTarget, transform.up);
            transform.rotation = Quaternion.Slerp(transform.rotation, newRotation, Time.fixedDeltaTime);


            float dot = Vector3.Dot(transform.forward, (target.position - transform.position).normalized);
            if (dot > .8f)
            {
                float step = speed * Time.fixedDeltaTime;
                Vector3 targetPos = new Vector3(target.position.x, target.position.y, target.position.z);
                transform.position = Vector3.MoveTowards(transform.position, targetPos, step);
                Debug.Log($"step is {step}");
            }
            yield return null;
        }
        SpellDamageEventV2(_cardInfo.gameObject, _cardData);
        Destroy(this.gameObject);
        //yield return new WaitForSeconds(1);


    }

    void SpellDamageEvent()
    {
        EnemyCharacter enenyCharacter = (EnemyCharacter) GameManager_BS.Instance.Enemy;
        enenyCharacter.TakeDamage(_cardData.value);
        GameEventsHub.SpellDamage.CardGO = _cardInfo.gameObject;
        GameEventsHub.SpellDamage.CardSO = _cardData;
        Event_SpellDamage.Raise();
        Destroy(this.gameObject);
    }

    void SpellDamageEventV2(GameObject cardObject, CardScriptableObject cardData)
    {
        EnemyCharacter enenyCharacter = (EnemyCharacter)GameManager_BS.Instance.Enemy;
        enenyCharacter.TakeDamage(_cardData.value);
        _cardEffectEvent.RaiseEvent(cardObject, cardData);
    }
}
