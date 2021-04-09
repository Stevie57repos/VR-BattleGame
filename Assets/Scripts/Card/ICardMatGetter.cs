using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICardMatGetter 
{
    // get default skin
    void SetDefaultMat(string cardtype);

    // get trigger material
    void SetTriggerMaterial();

    void CardSetUp(string cardName, string cardType);

}
