using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class SpellDamageHandler : MonoBehaviour, ICardEffect, ICardDataTransfer
{
    private CardScriptableObject _cardData = null;
    private CardController _cardInfo = null;

    private XRController controller;

    bool inAir = false;
    public Transform targetPos;
    [SerializeField] private float speed;
    [SerializeField] CardEffectEventChannelSO _cardEffectEvent;
    [SerializeField] CharacterRegistry _characterRegistry;
    [SerializeField] CardSelectionEventSO _cardSelectionEvent;

    private void Awake()
    {
        targetPos = _characterRegistry.CurrentEnemy.transform;
        inAir = false;
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
            StartCoroutine(MoveToTargetCoroutine(targetPos));
        }
        CheckDistance(targetPos);
    }

    IEnumerator MoveToTargetCoroutine(Transform target)
    {
        yield return new WaitForSeconds(1.5f);

        while (Vector3.Distance(transform.position, target.position) > 1.5f)
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
            }
            yield return null;
        }
    }

    void CheckDistance(Transform target)
    {
        if (Vector3.Distance(transform.position, target.position) < 1.5f)
        {
            SpellDamageEvent(_cardInfo.gameObject, _cardData);
            ResetPlayerCardSelection();
            Destroy(this.gameObject);
        }
    }
    void SpellDamageEvent(GameObject cardObject, CardScriptableObject cardData)
    {
        EnemyCharacter enenyCharacter = _characterRegistry.CurrentEnemy.GetComponent<EnemyCharacter>();
        enenyCharacter.TakeDamage(_cardData.value);
        _cardEffectEvent.RaiseEvent(cardObject, cardData);
    }
    void ResetPlayerCardSelection()
    {
        _cardSelectionEvent.RaiseEvent("None");
    }
    public void PassController(XRController controller)
    {
        this.controller = controller;
    }
}
