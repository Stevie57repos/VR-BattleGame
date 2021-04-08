using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CardGameObject : MonoBehaviour
{
	public CardScriptableObject cardData;
    public ClassScriptableObject cardOwner;
    public GameObject cardNameLabel;
    public GameObject cardCostLabel;
    public GameObject cardValueLabel;
    public ClassGameObject CardParentClass = null;

    public ICardMatGetter cardMatManager;


    public bool isActivated = false;
    public bool isComplete = false;

    void Start()
    {
        // grab the card material manager component
        cardMatManager = GetComponent<ICardMatGetter>();

        // Set card material based on type. Will need to change this to card Name in the future.
        SetCardMaterial();

        //Assign the parent Class Object
        CardParentClass = this.GetComponentInParent<ClassGameObject>();

        //Update Card UI
        name = cardData.name + " (Card)";
        cardOwner = CardParentClass.playerData;

        cardNameLabel.GetComponent<TextMeshProUGUI>().text = cardData.name;
        cardCostLabel.GetComponent<TextMeshProUGUI>().text = cardData.cost.ToString();
        cardValueLabel.GetComponent<TextMeshProUGUI>().text = cardData.value.ToString();
    }

    public void SetCardMaterial()
    {

        cardMatManager.SetDefaultMat(cardData.type.ToString());
    }


    //Activate corresponding method based on card type
    public void Activate()
    {
        switch (cardData.type)
        {
            case CardType.Attack:
                Attack();
                break;
            case CardType.Defend:
                Defend();
                break;
            case CardType.Spell:
                Spell();
                break;
            case CardType.Draw:
                Draw();
                break;
            case CardType.Curse:
                Curse();
                break;
            case CardType.Strength:
                Strength();
                break;
            case CardType.Energy:
                Energy();
                break;
        }
    }

    private void Attack()
    {
        int strength = CardParentClass.strength;
        int cardValue = cardData.value;
        int damage = cardValue + strength;
        // get enemy class object componenet and then run remove health method while passing through damage
        CardParentClass.GetTarget().RemoveHealth(damage);

    }

    //old testing code
    public int AttackDamage()
    {
        int strength = CardParentClass.strength;
        int cardValue = cardData.value;
        int damage = cardValue + strength;
        return damage;
    }

    public ClassGameObject PassTarget()
    {
       return CardParentClass.GetTarget();
    }

    private void Defend()
    {
        CardParentClass.AddHealth(cardData.value);
    }

    private void Spell()
    {
        CardParentClass.GetTarget().RemoveHealth(cardData.value);
        Debug.Log("spell has been used");
    }

    private void Draw()
    {
        this.GetComponentInParent<DeckGameObject>().Draw(cardData.value);
    }

    private void Curse()
    {
        CardParentClass.GetTarget().RemoveHealth(cardData.value);
        Debug.Log("curse has been used");
    }

    private void Strength()
    {
        CardParentClass.AddStrength(cardData.value);
    }

    private void Energy()
    {
        CardParentClass.BonusEnergy(cardData.value);
    }

    public void SpendEnergy_test2()
    {
        if (CardParentClass.SpendEnergy(cardData.cost))
        {
            Activate();
            if (cardData.singleUse == true)
            {
                this.GetComponentInParent<DeckGameObject>().Burn(cardData);
            }
            else
            {
                this.GetComponentInParent<DeckGameObject>().Play(cardData);
            }
            // Disable the card and fan remaining cards
            this.gameObject.SetActive(false);
            this.GetComponentInParent<DeckGameObject>().FanCards();
            Destroy(this.gameObject);
        }
        else
        {
            Debug.Log("Not enough energy.");
            //Reset card body position to hand
        }
    }

    public bool SpendEnergy()
    {
        Debug.Log($"CardParentClass energy is {CardParentClass.energy} and the card cost is {cardData.cost}");
        if (CardParentClass.SpendEnergy(cardData.cost))
        {
            return true;
        }
        else
        {
            Debug.Log("Not enough energy.");
            //Reset card body position to hand
            return false;
        }
    }


    // not being used
    public void CardGrab()
    {

        // Check if the card's owner is the turn's active player
        ClassScriptableObject activePlayer = GetComponentInParent<GameManagerObject>().GetActivePlayer();


        if (cardOwner == activePlayer) 
        {
            if (CardParentClass.SpendEnergy(cardData.cost)) 
            {
                Activate();
                if (cardData.singleUse == true) {
                    this.GetComponentInParent<DeckGameObject>().Burn(cardData);
                } else {
                    this.GetComponentInParent<DeckGameObject>().Play(cardData);
                }
                // Disable the card and fan remaining cards
                this.gameObject.SetActive(false);
                this.GetComponentInParent<DeckGameObject>().FanCards();
                Destroy(this.gameObject);
            } 
            else 
            {
                Debug.Log("Not enough energy.");
                //Reset card body position to hand
            }
        } else {
            Debug.Log("It's not your turn!");
            //Reset card body position to hand
        }
        
    }


    // Use the inspector to change the isActivated bool to use a card without VR tools
     void Update()
    {
        if (isActivated & !isComplete)
        {

            CardGrab();

            isComplete = true;
        }
    }

}
    
