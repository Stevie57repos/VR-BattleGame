using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Untitled Deck", menuName = "New Deck")]
public class DeckScriptableObject : ScriptableObject
{
	public new string name;
	public List<CardScriptableObject> cards;
}
