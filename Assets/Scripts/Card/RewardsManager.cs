using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewardsManager : MonoBehaviour
{
    public GameObject CardPrefab;
    public List<CardScriptableObject> CardsList;
    public List<GameObject> RewardsCardsList;
    private float _cardSpreadDistance;
    private Vector3 _cardSpawnPos;
    private void Awake()
    {
        _cardSpreadDistance = 0f;
    }
    public void SpawnCards(List<CardScriptableObject> cardsList, Vector3 spawnPos)
    {
        CardsList = cardsList;

        foreach (CardScriptableObject card in CardsList)
        {
            GameObject cardObject = Instantiate(CardPrefab);
            RewardsCardsList.Add(cardObject);
            _cardSpawnPos = spawnPos;
            cardObject.transform.position = new Vector3((_cardSpawnPos.x + _cardSpreadDistance), _cardSpawnPos.y, _cardSpawnPos.z);
            var cardController = cardObject.GetComponent<CardController>();
            cardController.SetupReward(this, card);
            _cardSpreadDistance += 0.25f;
        }
    }
    public void SpreadCards()
    {
        for(int i = 0; i < RewardsCardsList.Count; i++)
        {
            RewardsCardsList[i].transform.position = new Vector3((_cardSpawnPos.x + _cardSpreadDistance), _cardSpawnPos.y, _cardSpawnPos.z);
            _cardSpreadDistance += 0.25f;
        }
        _cardSpreadDistance = 0f;
    }
    public void CardSelected(GameObject cardGO)
    {
        foreach(GameObject card in RewardsCardsList)
        {
            if(card != cardGO)
            {
                card.SetActive(false);
            }
        }
    }
    public void CardUnselected(GameObject cardGO)
    {
        cardGO.SetActive(false);    
        SpreadCards();
        foreach (GameObject card in RewardsCardsList)
        {
            card.SetActive(true);        
        }
    }
    public void RemoveRewards()
    {
        foreach(GameObject card in RewardsCardsList)
        {
            Destroy(card);
        }
        Destroy(this.gameObject);
    }
}
