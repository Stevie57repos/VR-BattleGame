using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Untitled Class", menuName = "New Class")]
public class ClassScriptableObject : ScriptableObject
{
	public new string name;
	public DeckScriptableObject deck;
	public int health;
	public int maxHealth;
	public int maxEnergy;

}
