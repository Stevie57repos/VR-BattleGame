using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using TMPro;
using UnityEngine.XR;

public class RewardCardHandler : MonoBehaviour, ICardEffect
{
    [Header("UI Elements")]
    public GameObject CardDescriptionGO;
    private TextMeshProUGUI _cardDescription;
    public CardScriptableObject CardData;

    [SerializeField] private Timer _timer;
    [Header("Event")]
    [SerializeField] private GameManagerEventChannelSO WonEvent;

    private XRController _controller;
    private HapticsManager _hapticsManager;
    [SerializeField] private CharacterRegistry _characterRegistry;
    private RewardsManager _rewardsManager;

    private void Awake()
    {
        CardDescriptionGO.SetActive(false);    
        _cardDescription = CardDescriptionGO.GetComponentInChildren<TextMeshProUGUI>();
        _cardDescription.text = "Add this card to your deck";
        _timer = GetComponent<Timer>();

        if (_hapticsManager == null)
            _hapticsManager = GetComponent<HapticsManager>();
    }
    public void OnActivate()
    {

    }
    public void OnDeactivate()
    {

    }
    public void SetRewardsManager(RewardsManager rewardsManager)
    {
        _rewardsManager = rewardsManager;
    }
    public void OnHoverEntered()
    {
        CardDescriptionGO.SetActive(true);
    }
    public void OnHoverExited()
    {
        CardDescriptionGO.SetActive(false);
    }
    public void OnSelectEntered()
    {
        _timer.StartTimer(2f);
        _hapticsManager.TriggerHaptics(0.3f, 0.3f);
        _rewardsManager.CardSelected(this.gameObject);
        
    }
    public void OnSelectExited()
    {
        _timer.stopTimer();
        _rewardsManager.CardUnselected(this.gameObject);
    }
    public void PassController(XRController controller)
    {
        _hapticsManager.SetController(controller);
        _hapticsManager.TriggerHaptics(0.3f, 0.3f);      
    }
    public void LoadWinUI()
    {
        WonEvent.RaiseEvent();
    }
    public void RewardsRemoval()
    {
        _rewardsManager.RemoveRewards();
    }
    public void CardAddToDeck()
    {
        DeckManager deckManager = _characterRegistry.Player.GetComponent<DeckManager>();
        deckManager.AddToDeck(CardData);
    }
}
