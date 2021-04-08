using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public enum GameState { PlayerTurn, EnemyTurn, Lost, Won }

public class GameManagerObject : MonoBehaviour
{

	public GameState state;

	#region GameObject variables

	//Player prefab
	public GameObject playerPrefab;

	//transform targets for card effects
	public GameObject enemyModel;
	public GameObject centerStage;

	#endregion

    // Dictionary for returning the transform position of target game objects
    Dictionary<string, GameObject> posDictionary = new Dictionary<string, GameObject>();

    #region PlayerData

    //Public list which contains the player class data. Manually set in the inspector
    public List<ClassScriptableObject> playersData;
	
	//List of current players in the game.
	public List<GameObject> players;


	//variable containing class game object components for the players
	ClassGameObject player0 = null;
	ClassGameObject player1 = null;

	#endregion


	#region UI Elements

	//UI GameObjects
	public GameObject characterHealthLabel_Player1;
	public GameObject characterEnergyLabel_Player1;
	public GameObject characterHealthLabel_Player2;
	public GameObject characterEnergyLabel_Player2;
	public GameObject turnUI;

	//Textmesh component
	private TextMeshProUGUI turnUI_TextMeshPro;

	//Track turn
	public int turn;

	//Track which player current turn is
	public int activePlayer;

    #endregion

    void Start()
	{
		state = GameState.PlayerTurn; 

		// grab the textmeshcomponent
		turnUI_TextMeshPro = turnUI.GetComponent<TextMeshProUGUI>();

		// spawn players
		SpawnPlayers();

		// Set up dictionary for transform position of target game objects
		LoadDictionary();

		// Set initial Player UI panel values
		UpdateTurnUI();

		Debug.Log("this is currently " + playersData[activePlayer]);
	}


    #region Dictionary - Target Transform

    // Key is the card type string which returns the associated gameobject
    void LoadDictionary()
	{
		posDictionary.Add("Attack", enemyModel);
		posDictionary.Add("Defend", centerStage);
		posDictionary.Add("Spell", enemyModel);
		posDictionary.Add("Draw", centerStage);
		posDictionary.Add("Curse", enemyModel);
		posDictionary.Add("Strength", centerStage);
		posDictionary.Add("Energy", centerStage);
	}

	// returning the corresponding target transform by passing through the card type
	public Transform targetTransform(string cardType)
    {
		Transform targetPos = posDictionary[cardType].transform;
		return targetPos;
    }

    #endregion

    // this is a method to update the main game status UI in the middle
    void UpdateTurnUI()
    {
		turnUI_TextMeshPro.text = "Turn: " + turn + ", Active Player: " + GetPlayerName(activePlayer);
	}

	/// <summary>
	/// A temp method to return the name string value in the class game object of the player
	/// </summary>
	/// <param name="activePlayer"></param>
	/// <returns></returns>
	public string GetPlayerName(int activePlayer)
    {
		string ResultString = null;
		if (activePlayer == 0)
        {
			ResultString = player0.playerAssignment;
		}     		
        else if (activePlayer == 1)
        {
			ResultString = player1.playerAssignment;
		}
		return ResultString;	
    }
	
	void SpawnPlayers()
	{
		// instantiate player list
		players = new List<GameObject>();

		// Loop player creation based on the number of players data set in the inspector
		foreach (ClassScriptableObject playerData in playersData){
			Debug.Log("Loaded player: "+ playerData.name);

			GameObject currentEntity = Instantiate(playerPrefab, transform, false);

			currentEntity.GetComponent<ClassGameObject>().playerData = playerData;

			// add created player into players list
			players.Add(currentEntity);
		}
		
		player0 = players[0].GetComponent<ClassGameObject>();
		player1 = players[1].GetComponent<ClassGameObject>();

		// Manually set the targets for the players against each other and card spawn location
		player0.targetPlayer = players[1];
		player0.playerAssignment = "player1";
		player0.characterHealthLabel = characterHealthLabel_Player1;
		player0.characterEnergyLabel = characterEnergyLabel_Player1;
		player0.cardSpawn = new Vector3(0, 1, -2);

		player1.targetPlayer = players[0];
		player1.playerAssignment = "player2";
		player1.characterHealthLabel = characterHealthLabel_Player2;
		player1.characterEnergyLabel = characterEnergyLabel_Player2;
		player1.cardSpawn = new Vector3(0, 1, 2);
		
	}


	public ClassScriptableObject GetActivePlayer() {
		return playersData[activePlayer];
	}

	// GUI Button for PC use to End turn without the VR headset
    void OnGUI()
    {
        if (GUI.Button(new Rect(Screen.width / 2 - 50, 5, 100, 30), "End Turn"))
        {
			EndTurn();
		}
    }

    #region End Turn Methods

    // Method that runs all methods needed to end the turn
    public void EndTurn()
    {
		PlayerTurnEnd();
		NextPlayer();
		PlayerTurnStart();
		UpdateTurnUI();
	}

    private void PlayerTurnEnd() 
	{
    	Debug.Log(players[activePlayer].name +" ended their turn!");
    }

	void NextPlayer()
	{
		activePlayer++;
		if (activePlayer >= players.Count)
		{
			activePlayer = 0;
			turn++;
		}
		Debug.Log("Turn: " + turn + ", Active Player: " + players[activePlayer].name);
	}

	private void PlayerTurnStart() 
	{
    	Debug.Log(players[activePlayer].name +"'s turn!");
    	players[activePlayer].GetComponentInChildren<DeckGameObject>().SuperDraw();
        players[activePlayer].GetComponent<ClassGameObject>().RefreshEnergy();

    }

    #endregion
}
