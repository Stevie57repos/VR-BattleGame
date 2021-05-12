using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempSoundController : MonoBehaviour
{
    private AudioSource _audioSource;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    public void PlayTempSound(AudioClip soundClip, Transform soundPosition, float SecondsBeforeDestruction)
    {
        _audioSource.clip = soundClip;
        transform.position = soundPosition.position;
        _audioSource.Play();
        Destroy(this.gameObject, SecondsBeforeDestruction);
    }
}
