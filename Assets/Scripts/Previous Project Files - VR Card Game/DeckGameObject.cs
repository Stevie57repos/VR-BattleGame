using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeckGameObject : MonoBehaviour
{
	public GameObject cardPrefab;
	public DeckScriptableObject deckData;

	public List<CardScriptableObject> deck;
	public List<CardScriptableObject> hand;
	public List<CardScriptableObject> graveyard;
	public List<CardScriptableObject> burn;

	private int maxHandSize = 5;

	private void Start()
	{
		name = deckData.name;
		Load();
		Shuffle();
		Draw(5);
	}

	private void Load()
	{
		Debug.Log(deckData.cards.Count.ToString() + " cards loaded into deck!");
		foreach (CardScriptableObject card in deckData.cards)
		{
			deck.Add(card);
		}
	}

	public void Shuffle()
	{
		Debug.Log("Shuffling the deck!");
		for (int t = 0; t < deck.Count; t++ ) {
			CardScriptableObject tmp = deck[t];
			int r = Random.Range(t, deck.Count);
			deck[t] = deck[r];
			deck[r] = tmp;
		} 
	}

	public CardScriptableObject GetNextCard()
	{
		// Next card mechanics
		return deck[0];
	}

	public void Draw(int n=1)
	{
		for (int i=0; i < n; i++){
			// No cards to draw
			if (deck.Count == 0) {
				Debug.Log("No more cards to draw!");
				break;
			} else {
				// Hand size check
				CardScriptableObject card = GetNextCard();
				if (hand.Count > maxHandSize){
					Overdraw(card);
				} else {
					// Draw the card
					Debug.Log("Drew a " + card.name + " card!");
					SpawnCard(card);
					hand.Add(card);
					deck.Remove(card);
				}
			}
		}
		FanCards();
	}

	public void FanCards() {
		CardGameObject[] allChildren = GetComponentsInChildren<CardGameObject>();
		for (int i=0; i < allChildren.Length; i++) {
			// 0.25f is the card spacing
			allChildren[i].gameObject.transform.localPosition = new Vector3( (0.25f * i) , 0, 0);
		}
	}

	// Will Refill the deck and draw if deck is empty
	public void SuperDraw(int n=1)
	{
		if (deck.Count == 0) {
			Refill();
		}
		Draw(n);
	}

	public void SpawnCard(CardScriptableObject card)
	{
		GameObject currentEntity = Instantiate(cardPrefab, transform, false);
		
		//GameObject currentEntity = Instantiate(cardPrefab, new Vector3(0,0,0), Quaternion.identity, transform); 

		currentEntity.GetComponent<CardGameObject>().cardData = card;
	}

	public void Play(CardScriptableObject card)
	{
		Debug.Log("Played a " + card.name + " card!");
		graveyard.Add(card);
		hand.Remove(card);
	}

	public void Overdraw(CardScriptableObject card)
	{
		Debug.Log("Overdrew a " + card.name + " card! It was sent to the graveyard!");
		graveyard.Add(card);
		deck.Remove(card);
	}

	public void Refill()
	{
		Debug.Log("Refilled the deck with "+ graveyard.Count.ToString() +" cards from graveyard!");
		foreach (CardScriptableObject card in graveyard) {
			deck.Add(card);
		}
		graveyard.Clear();
		Shuffle();
	}

	public void Burn(CardScriptableObject card) {
		Debug.Log("Burned a " + card.name + " card! It was sent to the burned pile It will not be refilled!");
		burn.Add(card);
		hand.Remove(card);
	}

	public void AddToHand(CardScriptableObject card) {

	}

}
