using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using TMPro;
using UnityEngine.XR;

public class RewardCardHandler : MonoBehaviour, ICardEffect
{
    public GameObject _cardDescriptionGO;
    private TextMeshProUGUI _cardDescription;
    public CardScriptableObject CardData;

    private RewardsManager _rewardsManager;

    [SerializeField] private Timer _timer;
    public GameManagerEventChannelSO WonEvent;

    private XRController _controller;
    [SerializeField] private HapticsManager _hapticsManager;

    [SerializeField] CharacterRegistry _characterRegistry;
    private DeckManager _playerDeck;
    private void Awake()
    {
        _cardDescriptionGO.SetActive(false);    
        _cardDescription = _cardDescriptionGO.GetComponentInChildren<TextMeshProUGUI>();
        _cardDescription.text = "RANDOM DESCRIPTIVE TEXT";
        _timer = GetComponent<Timer>();

        if (_hapticsManager == null)
            _hapticsManager = GetComponent<HapticsManager>();

        if (_playerDeck == null)
            _playerDeck = _characterRegistry.Player.GetComponent<DeckManager>();
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
        _cardDescriptionGO.SetActive(true);
    }

    public void OnHoverExited()
    {
        _cardDescriptionGO.SetActive(false);
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
}
