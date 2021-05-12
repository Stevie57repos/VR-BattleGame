using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum CardType {
	Attack,
	Defend,
	Spell,
	Draw,
	Curse,
	Strength,
	Energy
}

[System.Serializable]
[CreateAssetMenu(fileName = "Untitled Card", menuName = "New Card")]
public class CardScriptableObject : ScriptableObject
{
	public new string name;
	public CardType type;
	public int value;
	
	public int cost;

	public bool singleUse;

	public AudioClip CardSpawn;
	public AudioClip soundRelease;
	public AudioClip soundFlying;
	public AudioClip soundHit;
}
