using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Events/Card Selection Event Channel")]
public class CardSelectionEventSO : ScriptableObject
{
    public UnityAction<string> OnCardSelection;

    public void RaiseEvent(string cardType)
    {
        if(OnCardSelection != null)
        {
            OnCardSelection.Invoke(cardType);
        }
        else
        {
            Debug.LogWarning("An OnCardSelection was requested but nobody picked it up");
        }
    }
}
