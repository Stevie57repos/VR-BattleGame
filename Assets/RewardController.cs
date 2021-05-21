using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class RewardController : MonoBehaviour
{ 
    public List<CardScriptableObject> RewardCards;
    [SerializeField] private GameObject _cardPrefab;
    [SerializeField] CharacterRegistry _characterRegistry;


    float cardSpreadDistance;

    public List<Transform> SpawnCardLocation;

    private void Awake()
    {
        SpawnCardLocation[0] = _characterRegistry.Player.GetComponent<DeckManager>().CardspawnSpotGO.transform;
        cardSpreadDistance = 0f;
    }

    public void SpawnCards()
    {
        foreach (CardScriptableObject card in RewardCards)
        {
            GameObject cardObject = Instantiate(_cardPrefab);
            Vector3 cardSpawnPos = SpawnCardLocation[0].position;
            cardObject.transform.position = new Vector3((cardSpawnPos.x + cardSpreadDistance), cardSpawnPos.y, cardSpawnPos.z);
            var cardController = cardObject.GetComponent<CardController>();
            cardController.CardData = card;
            cardController.DeckManager = null;
            cardSpreadDistance += 0.25f;
        }
    }  
}
