using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class EnemyRewardController : MonoBehaviour
{ 
    public List<CardScriptableObject> RewardCards;
    [SerializeField] private GameObject _prefabRewardCardsGO;
    [SerializeField] CharacterRegistry _characterRegistry;

    public void TriggerRewards() 
    {
        GameObject RewardCardsGO = Instantiate(_prefabRewardCardsGO);
        Vector3 cardSpawnPos = _characterRegistry.Player.GetComponent<DeckManager>().CardspawnSpotGO.transform.position;
        var RewardsManager = RewardCardsGO.GetComponent<RewardsManager>();
        RewardsManager.SpawnCards(RewardCards, cardSpawnPos);
    }

    //public void SpawnCards()
    //{
    //    foreach (CardScriptableObject card in RewardCards)
    //    {
    //        GameObject cardObject = Instantiate(_cardPrefab);
    //        Vector3 cardSpawnPos = SpawnCardLocation[0].position;
    //        cardObject.transform.position = new Vector3((cardSpawnPos.x + cardSpreadDistance), cardSpawnPos.y, cardSpawnPos.z);
    //        var cardController = cardObject.GetComponent<CardController>();
    //        cardController.CardData = card;
    //        cardController.DeckManager = null;
    //        cardSpreadDistance += 0.25f;
    //    }
    //}
}
