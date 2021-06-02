using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AttackTypes { Basic, Special };
public enum SpawnLoactions { Left, Right, Top };

[CreateAssetMenu(menuName = "PluggableAI/Actions/BasicAttackData")]
public class BasicAttackData : ScriptableObject
{
    public SpawnLoactions SpawnLocation;
    public AttackTypes AttackType;
    public int ProjectileNumber;
    public float TimeBetweenProjectiles;
    public float WaitBeforeAttackBegins;
    public float WaitAfterAttackEnds;
    public string Text;
}


