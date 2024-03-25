using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class InterstitialAds : MonoBehaviour, IUnityAdsLoadListener, IUnityAdsShowListener
{
    public static InterstitialAds Instance;

    [SerializeField] private string androidAdID = "Interstitial_Android";
    [SerializeField] private string iOSAdID = "Interstitial_iOS";

    [SerializeField] private float _timer;
    [SerializeField] private float _currentTimer;
    [SerializeField] private bool _isAdEnabled;

    private string adID;

    private void Start()
    {
        if (PlayerPrefs.GetInt("AdEnabled", 1) == 1)
            _isAdEnabled = true;
        else
            _isAdEnabled = false;
    }
    void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(this);
        else
            Instance = this;

        DontDestroyOnLoad(gameObject);

        adID = (Application.platform == RuntimePlatform.IPhonePlayer)
            ? iOSAdID
            : androidAdID;
        LoadAd();
    }
    private void FixedUpdate()
    {
        if (_isAdEnabled)
        {
            if (_currentTimer < _timer)
                _currentTimer += Time.fixedDeltaTime;
            else
                ShowAd();
        }
    }
    public void LoadAd()
    {
        if (PlayerPrefs.GetInt("AdEnabled", 1) == 1)
        {
            Debug.Log("Loading Ad: " + adID);
            Advertisement.Load(adID, this);
        }
    }

    public void ShowAd()
    {
        if(PlayerPrefs.GetInt("AdEnabled", 1) == 1)
        {
            Debug.Log("Showing Ad: " + adID);
            Advertisement.Show(adID, this);
        }
    }

    public void OnUnityAdsShowFailure(string placementId, UnityAdsShowError error, string message)
    {
    }

    public void OnUnityAdsShowStart(string placementId)
    {
    }

    public void OnUnityAdsShowClick(string placementId)
    {
    }

    public void OnUnityAdsShowComplete(string placementId, UnityAdsShowCompletionState showCompletionState)
    {
        _currentTimer = 0f;
        LoadAd();
    }

    public void OnUnityAdsAdLoaded(string placementId)
    {
    }

    public void OnUnityAdsFailedToLoad(string placementId, UnityAdsLoadError error, string message)
    {
    }
}