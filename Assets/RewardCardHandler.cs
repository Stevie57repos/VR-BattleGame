using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using TMPro;

public class RewardCardHandler : MonoBehaviour, ICardEffect
{

    public GameObject _cardDescriptionGO;
    private TextMeshProUGUI _cardDescription;

    [SerializeField] private Timer _timer;
    public GameManagerEventChannelSO WonEvent;


    private void Awake()
    {
        _cardDescriptionGO.SetActive(false);    
        _cardDescription = _cardDescriptionGO.GetComponentInChildren<TextMeshProUGUI>();
        _cardDescription.text = "RANDOM TEXT";
        _timer = GetComponent<Timer>();
    }
    public void OnActivate()
    {

    }

    public void OnDeactivate()
    {

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
    }

    public void OnSelectExited()
    {
        _timer.stopTimer();
    }

    public void PassController(XRController controller)
    {

    }

    public void LoadWinUI()
    {
        WonEvent.RaiseEvent();
    }
}
