using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffect : MonoBehaviour
{
    private AudioSource _audioSource;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        _audioSource.playOnAwake = true;
    }
    public void PlaySound(AudioClip clip, float volume = 1f)
    {
        _audioSource.volume = volume;
        _audioSource.clip = clip;

        _audioSource.Play();

        Invoke("SelfDestroy", clip.length);
    }
    private void SelfDestroy()
    {
        Destroy(gameObject);
    }
}
