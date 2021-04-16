using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spell_Damage : MonoBehaviour, ICardEffect, ICardDataTransfer
{
    private CardScriptableObject _cardData = null;
    private CardController _cardInfo = null;

    bool inAir = false;
    public Transform targetPos;
    [SerializeField] private float speed;
    private Rigidbody _rigidBody;

    private void Awake()
    {
        targetPos = GameManager_BS.Instance.enemyManager.currentEnemyGO.transform;
        _rigidBody = GetComponent<Rigidbody>();
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
                float step = speed * Time.deltaTime;
                Vector3 targetPos = new Vector3(target.position.x, target.position.y, target.position.z);
                transform.position = Vector3.MoveTowards(transform.position, targetPos, step);
                Debug.Log($"step is {step}");
            }
            yield return null;
        }
    }


}
