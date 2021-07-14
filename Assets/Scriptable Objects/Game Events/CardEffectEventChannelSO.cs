using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Events/CardEffect Event Channel")]
public class CardEffectEventChannelSO : ScriptableObject
{
    public UnityAction<GameObject, CardScriptableObject> OnCardEffectActivate;
    public void RaiseEvent(GameObject cardOjbect, CardScriptableObject cardData)
    {
        if(OnCardEffectActivate != null)
        {
            OnCardEffectActivate.Invoke(cardOjbect, cardData);
        }
        else
        {
            Debug.LogWarning("An OnCardEffect was requested but nobody picked it up");
        }
    }
}
