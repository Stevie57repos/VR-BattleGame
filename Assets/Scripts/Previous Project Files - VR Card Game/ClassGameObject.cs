using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


[System.Serializable]
public class ClassGameObject : MonoBehaviour
{
    //prefab for the card deck. Assigned in inspector
	public GameObject deckPrefab;
    
    // this is assigned automatically in game manager
	public ClassScriptableObject playerData;
    private GameManagerObject gameManager;

    // tracker for whether this class is player1 or player2
    public string playerAssignment = null;

    // this is assigned in the Game manager. Represents the Enemy player
    public GameObject targetPlayer;

    #region Class Player Variables

    public int health;
    public int maxHealth;

    public int energy;
    public int maxEnergy;

    public int strength;

    #endregion

    #region UI elements
    // UI element reference
    public GameObject characterHealthLabel;
    public GameObject characterEnergyLabel;

    //spawn location
    public Vector3 cardSpawn;

    #endregion

    private void Start()
    {
        // assign gameManager value
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManagerObject>();

        // playerData was set in GameManager. Copy values to this ClassGAmeObject variables
        this.name = playerData.name + " (Class)";
        this.health = playerData.maxHealth;
        this.maxHealth = playerData.maxHealth;
        this.energy = playerData.maxEnergy;
        this.maxEnergy = playerData.maxEnergy;
        this.strength = 0;

    	SpawnDeck();
        UpdateUI_Panels();


    }

    private void UpdateUI_Panels()
    {
        characterHealthLabel.GetComponent<TextMeshProUGUI>().text = health.ToString();
        characterEnergyLabel.GetComponent<TextMeshProUGUI>().text = energy.ToString();
    }

    public ClassGameObject GetTarget()
    {
        return targetPlayer.GetComponent<ClassGameObject>();
    }

    public void SpawnDeck()
    {
        GameObject currentEntity = Instantiate(deckPrefab, cardSpawn, Quaternion.identity, transform);
        currentEntity.GetComponent<DeckGameObject>().deckData = playerData.deck;
    }

    public void AddStrength(int n)
    {
        strength += n;
    }
    
    public void AddHealth(int n)
    {
        if (health + n >= maxHealth) {
            health = maxHealth;
            Debug.Log($"Health has been added but we are at max health");
        } else {
            health += n;
        }

        UpdateUI_Panels();
    }

    public void RemoveHealth(int n)
    {
        if (health - n <= 0) 
        {
            health = 0;
        } 
        else 
        {
            health -= n;
        }

        UpdateUI_Panels();

    }

    public void RefreshEnergy()
    {
        //maxEnergy += 1;
        energy = maxEnergy;

        UpdateUI_Panels();
    }

    public void BonusEnergy(int n)
    {
        // Energy only for this turn
        energy += n;

        UpdateUI_Panels();
    }

    public bool SpendEnergy(int n)
    {
        if (energy - n > 0) 
        {
            energy -= n;
            UpdateUI_Panels();
            return true;
        } else {
            return false;
        }
    }

    // am I the computer . Check Player assignment
    bool CheckIfEnemyAI()
    {
        if (playerAssignment == "player2")
        {
            Debug.Log($"{this.gameObject.name} is player2");
            return true;
        }
        else
            return false;
    }

    bool CheckMyTurn()
    {
        if(playerAssignment == gameManager.GetPlayerName(gameManager.activePlayer))
        {
            Debug.Log($"{this.name} : It is currently my turn");
            return true;
        }
        else
        {
            Debug.Log($"{this.name} : It is not my turn");
            return false;
        }
    }


    private void Update()
    {
        if(CheckIfEnemyAI() && CheckMyTurn())
        {
            Debug.Log("It is the computer's turn");
        }
    }


    // If i am the computer then is it my turn ?

    // if it is my turn. Evaluate my list of cards in my hand 

    // run a bool test if the cost of the card is less than my energy available. If I have enough energy add it to the to the command list. Move on to the next card in my hand.

    // After I'm done adding to the command list. Execute a coroutien to play each card in my command list.





}
