using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/Actions/TestAttackData")]
public class TestAttackData : ScriptableObject
{
    public int ProjectileNumber;
    public float TimeBetweenProjectiles;
    public string Text;
    public float WaitBeforeAttackBegins;
    public float WaitAfterAttackEnds;
}
