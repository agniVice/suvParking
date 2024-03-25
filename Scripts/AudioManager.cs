using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    public bool IsAudioEnabled;
    public bool IsMusicEnabled;

    [SerializeField] private AudioClip _carCrash;
    [SerializeField] private AudioClip _coinPickUp;
    [SerializeField] private AudioClip _carDanger;

    [SerializeField] private List<AudioSource> _audio;
    [SerializeField] private List<AudioSource> _music;

    private void Awake()
    {
        if (Instance != this && Instance != null)
            Destroy(this);
        else
            Instance = this;
    }
    private void Start()
    {
        Initialization();
    }
    private void Initialization()
    {
        if (PlayerPrefs.GetInt("IsAudioEnabled", 1) == 1)
        {
            IsAudioEnabled = true;
            if (_audio.Count != 0)
            {
                foreach (AudioSource source in _audio)
                    source.mute = false;
            }
        }
        else
        {
            IsAudioEnabled = false;
            if (_audio.Count != 0)
            {
                foreach (AudioSource source in _audio)
                    source.mute = true;
            }
        }

        if (PlayerPrefs.GetInt("IsMusicEnabled", 1) == 1)
        {
            IsMusicEnabled = true;
            if (_music.Count != 0)
            {
                foreach (AudioSource source in _music)
                    source.mute = false;
            }
        }
        else
        {
            IsMusicEnabled = false;
            if (_music.Count != 0)
            {
                foreach (AudioSource source in _music)
                    source.mute = true;
            }
        }
    }
    public void PlaySound(string soundName, float volume = 1f)
    {
        if (!IsAudioEnabled)
            return;
        SoundEffect sfx = Instantiate(new GameObject()).AddComponent<AudioSource>().gameObject.AddComponent<SoundEffect>();

        _audio.Add(sfx.GetComponent<AudioSource>());

        if (!IsAudioEnabled)
            sfx.GetComponent<AudioSource>().mute = true;

        switch (soundName)
        {
            case "CarCrash":
                sfx.PlaySound(_carCrash, volume);
                break;
            case "CoinPickup":
                sfx.PlaySound(_coinPickUp, volume);
                break;
            case "CarDanger":
                sfx.PlaySound(_carDanger, volume);
                break;
        }
    }
    public void ToggleAudio()
    {
        IsAudioEnabled = !IsAudioEnabled;
        MenuUI.Instance.UpdateSettings();
        SaveData();
        Initialization();
    }
    public void ToggleMusic()
    {
        IsMusicEnabled = !IsMusicEnabled;
        MenuUI.Instance.UpdateSettings();
        SaveData();
        Initialization();
    }
    private void SaveData()
    {
        if (IsAudioEnabled)
            PlayerPrefs.SetInt("IsAudioEnabled", 1);
        else
            PlayerPrefs.SetInt("IsAudioEnabled", 0);

        if (IsMusicEnabled)
            PlayerPrefs.SetInt("IsMusicEnabled", 1);
        else
            PlayerPrefs.SetInt("IsMusicEnabled", 0);
    }
    public void AddSound(AudioSource source)
    {
        _audio.Add(source);

        if (IsAudioEnabled)
            source.mute = true;
    }
}
