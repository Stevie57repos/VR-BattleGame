using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class SpellHealHandler : MonoBehaviour, ICardEffect, ICardDataTransfer
{
    [SerializeField] private MeshRenderer _meshRend;
    [SerializeField] CharacterRegistry _characterRegistry;
    private HapticsManager _hapticsManager;
    private CardScriptableObject _cardData = null;
    private CardController _cardInfo = null;
    private PlayerCharacter _player;

    [Header("Audio")]
    [SerializeField] AudioSource _audioSource;
    [SerializeField] AudioClip _spellStartSound;
    [SerializeField] AudioClip _spellTriggerSound;

    [Header("Events")]
    [SerializeField] CardEffectEventChannelSO _cardEffectEvent;
    [SerializeField] CardSelectionEventSO _cardSelectionEvent;
    private void Awake()
    {
        _player = _characterRegistry.Player.GetComponent<PlayerCharacter>();
        _audioSource = GetComponent<AudioSource>();
        _audioSource.PlayOneShot(_spellStartSound);;
    }
    public void TransferCardData(CardController cardInfo)
    {
        _cardInfo = cardInfo;
        _cardData = cardInfo.CardData;
    }
    public void OnActivate()
    {

    }
    public void OnDeactivate()
    {

    }
    public void OnHoverEntered()
    {

    }
    public void OnHoverExited()
    {

    }
    public void OnSelectEntered()
    {
        Invoke("SpellHealEvent", 5f);
    }
    public void OnSelectExited()
    {
        SpellHealEvent();
    }
    private void SpellHealEvent()
    {
        _meshRend.enabled = false;
        _audioSource.Stop();
        _audioSource.PlayOneShot(_spellTriggerSound);
        Debug.Log($"player is {_player.name}");
        Debug.Log($"_card data value is {_cardData.value}");
        _player.HealHealth(_cardData.value);
        _cardEffectEvent.RaiseEvent(_cardInfo.gameObject, _cardData);
        ResetPlayerCardSelection();
        Destroy(this.gameObject,1.25f);
        _hapticsManager.TriggerHaptics(0.7f, 0.3f);
    }
    private void ResetPlayerCardSelection()
    {
        _cardSelectionEvent.RaiseEvent("None");
    }
    public void PassController(XRController controller)
    {
        if (_hapticsManager == null)
            _hapticsManager = GetComponent<HapticsManager>();

        _hapticsManager.SetController(controller);
        _hapticsManager.TriggerHaptics(0.3f, 0.3f);
    }
}
