using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardEffect_Base : MonoBehaviour, ICardObjectGetter
{
    private Transform target;
    
    private float speed = 10f;

    GameManagerObject gameManager;

    // contains the card data
    private CardGameObject CardObject;

    // Start is called before the first frame update
    private void Start()
    {
        //find and assign the gameManager
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManagerObject>();

        SetTarget(CardObject.cardData.type.ToString());

        StartCoroutine(MoveToTargetCoroutine(target));
    }

    public void GetCardObject(CardGameObject cardObjectData)
    {
        CardObject = cardObjectData;
    }

    public void SetTarget(string cardTypString)
    {

       target = gameManager.targetTransform(cardTypString);
       
       if (target == null)       
        {
            Debug.Log($"target is currently null");
        }
    }

    // Movement method
    IEnumerator MoveToTargetCoroutine(Transform target)
    {
        // hold spawn position for duration set in parameter
        yield return new WaitForSeconds(1f);

        // start moving while the distance is greater than 1 feet
        while (Vector3.Distance(transform.position, target.position) > 1f)
        {
            // rotate the sword to face the target
            Vector3 lookatpos = target.position - transform.position;
            Quaternion newrotation = Quaternion.LookRotation(lookatpos, transform.up);
            Quaternion targetrotation = new Quaternion(0, newrotation.y, 0, 0);
            transform.rotation = Quaternion.Slerp(transform.rotation, newrotation, Time.deltaTime);


            //check direction rotation is facing before moving
            float dot = Vector3.Dot(transform.forward, (target.position - transform.position).normalized);
            if (dot > .8f)
            {
                float step = speed * Time.deltaTime;
                Vector3 targetPos = new Vector3(target.position.x, target.position.y, target.position.z);
                transform.position = Vector3.MoveTowards(transform.position, targetPos, step);
            }

            yield return null;
        }

        CardEffectActivate();
    }

    private void CardEffectActivate()
    {
        // using Activate in CardGameObject
        Debug.Log("cardeffect has been activated");
        Debug.Log(CardObject.CardParentClass);


        CardObject.Activate();

        //CardObject.CardGrab();
        Destroy(this.gameObject, 3f);
    }


}
