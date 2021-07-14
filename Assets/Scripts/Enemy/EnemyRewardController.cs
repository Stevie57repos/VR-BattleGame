using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class EnemyRewardController : MonoBehaviour
{ 
    public List<CardScriptableObject> RewardCards;
    [SerializeField] private GameObject _prefabRewardCardsGO;
    [SerializeField] private CharacterRegistry _characterRegistry;
    private bool isRewardTriggered;
    private void Awake()
    {
        isRewardTriggered = false;
    }
    public void TriggerRewards() 
    {
        if (!isRewardTriggered)
        {
            isRewardTriggered = true;
            GameObject RewardCardsGO = Instantiate(_prefabRewardCardsGO);
            Vector3 cardSpawnPos = _characterRegistry.Player.GetComponent<DeckManager>().CardspawnSpotGO.transform.position;
            var RewardsManager = RewardCardsGO.GetComponent<RewardsManager>();
            RewardsManager.SpawnCards(RewardCards, cardSpawnPos);
        }
    }
}
