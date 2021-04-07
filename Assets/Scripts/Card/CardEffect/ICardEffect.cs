using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICardEffect 
{
    // Set target gameobject transform method
    void SetTarget(string cardType);

    void CardEffectActivate();

}
