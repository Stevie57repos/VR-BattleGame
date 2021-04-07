using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Game Manager", menuName = "New Game Manager")]
public class GameManagerScriptableObject : ScriptableObject
{
	public List<ClassScriptableObject> players;
}
