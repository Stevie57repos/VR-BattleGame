using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewardsManager : MonoBehaviour
{
    public GameObject _cardPrefab;
    public List<CardScriptableObject> _cardsList;
    public List<GameObject> _rewardsCardsList;
    private float cardSpreadDistance;
    private Vector3 cardSpawnPos;
    private void Awake()
    {
        cardSpreadDistance = 0f;
    }
    public void SpawnCards(List<CardScriptableObject> cardsList, Vector3 spawnPos)
    {
        _cardsList = cardsList;

        foreach (CardScriptableObject card in _cardsList)
        {
            GameObject cardObject = Instantiate(_cardPrefab);
            _rewardsCardsList.Add(cardObject);
            cardSpawnPos = spawnPos;
            cardObject.transform.position = new Vector3((cardSpawnPos.x + cardSpreadDistance), cardSpawnPos.y, cardSpawnPos.z);
            var cardController = cardObject.GetComponent<CardController>();
            cardController.SetupReward(this, card);
            cardSpreadDistance += 0.25f;
        }
    }
    public void SpreadCards()
    {
        for(int i = 0; i < _rewardsCardsList.Count; i++)
        {
            _rewardsCardsList[i].transform.position = new Vector3((cardSpawnPos.x + cardSpreadDistance), cardSpawnPos.y, cardSpawnPos.z);
            cardSpreadDistance += 0.25f;
        }
        cardSpreadDistance = 0f;
    }
    public void CardSelected(GameObject cardGO)
    {
        foreach(GameObject card in _rewardsCardsList)
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
        foreach (GameObject card in _rewardsCardsList)
        {
            card.SetActive(true);        
        }
    }
    public void RemoveRewards()
    {
        foreach(GameObject card in _rewardsCardsList)
        {
            Destroy(card);
        }
        Destroy(this.gameObject);
    }
}
