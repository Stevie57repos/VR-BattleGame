using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewSoundsListSO", menuName = "ScriptableObjects/SoundsListSO")]
public class SoundsListSO : ScriptableObject
{
    public AudioClip[] SoundsArray;
}
