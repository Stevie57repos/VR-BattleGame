using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class DefenseShieldHandler : MonoBehaviour, ICardDataTransfer, ICardEffect
{
    [SerializeField] private int _currDurability = 2;
    [SerializeField] PlayerCharacter _playerCharacter;

    private XRController controller;

    private HapticsManager _hapticsManager;

    private CardScriptableObject _cardData = null;
    private CardController _cardInfo = null;

    //public GameEvent ShieldDestroyed;
    [SerializeField] CharacterRegistry _characterRegistry;
    [SerializeField] CardEffectEventChannelSO _cardEffectEvent;
    [SerializeField] CardSelectionEventSO _cardSelectionEvent;

    private AudioSource _audioSource;
    [SerializeField] AudioClip _ShieldSummonAudioClip;
    [SerializeField] SoundsListSO _projectileDestructionAudio;
    [SerializeField] SoundsListSO _shieldBlockAudio;
    [SerializeField] AudioClip _droppedSheildAudioClip;

    [SerializeField] PlayerSettings _playerSettings;

    [SerializeField] GameObject _shieldModelGO;
    private MeshRenderer _shieldMeshRender;
    private BoxCollider _collider;

    public void TransferCardData(CardController cardInfo)
    {
        _cardInfo = cardInfo;
        _cardData = cardInfo.CardData;
    }
    private void Awake()
    {
        _playerCharacter = _characterRegistry.Player.GetComponent<PlayerCharacter>();
        _audioSource = GetComponent<AudioSource>();
        _audioSource.PlayOneShot(_ShieldSummonAudioClip);
        _shieldMeshRender = _shieldModelGO.GetComponent<MeshRenderer>();
        _collider = _shieldModelGO.GetComponent<BoxCollider>();
        _hapticsManager = GetComponent<HapticsManager>();
    }

    private void OnCollisionEnter(Collision other)
    {
        Debug.Log("collision detected");
        if (other.gameObject.GetComponent<Enemy_projectile>())
        {
            var enemyProjectile = other.gameObject.GetComponent<Enemy_projectile>();
            int chargeValue = enemyProjectile._chargeValue;

            _playerCharacter.IncreaseMana(chargeValue);

            _audioSource.PlayOneShot(RandomAudioClip(_projectileDestructionAudio));
            CheckShieldDurability(chargeValue);
            _audioSource.PlayOneShot(RandomAudioClip(_shieldBlockAudio));
            _hapticsManager.TriggerHaptics(0.7f, 0.3f);
            enemyProjectile.gameObject.SetActive(false);
        }
        else if (other.gameObject.CompareTag("Ground"))
        {
            _cardEffectEvent.RaiseEvent(_cardInfo.gameObject, _cardData);
            ResetPlayerCardSelection();
            _audioSource.PlayOneShot(_droppedSheildAudioClip);
            Destroy(this.gameObject, 1.25f);
        }
    }

    private AudioClip RandomAudioClip(SoundsListSO soundsListSO)
    {
        AudioClip randomClip = soundsListSO.SoundsArray[UnityEngine.Random.Range(0, soundsListSO.SoundsArray.Length)];
        return randomClip;
    }

    void CheckShieldDurability(int chargeValue)
    {
        _currDurability -= chargeValue;

        if (_currDurability <= 0)
        {
            _cardEffectEvent.RaiseEvent(_cardInfo.gameObject, _cardData);
            _audioSource.PlayOneShot(_droppedSheildAudioClip);
            _hapticsManager.TriggerHaptics(0.85f, 0.3f);
            _shieldMeshRender.enabled = false;
            _collider.enabled = false;
            ResetPlayerCardSelection();
            Destroy(this.gameObject, 1.5f);           
        }
    }

    void ResetPlayerCardSelection()
    {
        _cardSelectionEvent.RaiseEvent("None");
        _hapticsManager.ClearController();
    }
    public void OnHoverEntered()
    {

    }

    public void OnHoverExited()
    {

    }

    public void OnSelectEntered()
    {

    }

    public void OnSelectExited()
    {

    }

    public void OnActivate()
    {

    }

    public void OnDeactivate()
    {

    }

    public void PassController(XRController controller)
    {
        if (_hapticsManager == null)
            _hapticsManager = GetComponent<HapticsManager>();

        _hapticsManager.SetController(controller);
        _hapticsManager.TriggerHaptics(0.3f, 0.3f);
    }
}