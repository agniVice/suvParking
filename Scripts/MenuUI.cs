using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public class MenuUI : MonoBehaviour
{
    public static MenuUI Instance;

    [SerializeField] private float _speedUITween;

    [SerializeField] private CanvasGroup _canvasMain;
    [SerializeField] private CanvasGroup _canvasShop;
    [SerializeField] private CanvasGroup _canvasSettings;

    [SerializeField] private TextMeshProUGUI _money;
    [SerializeField] private TextMeshProUGUI _carPrice;

    [SerializeField] private Image _speedBar;
    [SerializeField] private Image _brakeBar;
    [SerializeField] private Image _mobilityBar;

    [SerializeField] private GameObject[] _arrows;
    [SerializeField] private GameObject _lock;

    [SerializeField] private Button _disableAd;
    [SerializeField] private Image _sound;
    [SerializeField] private Image _music;

    [SerializeField] private Sprite _enabledSprite;
    [SerializeField] private Sprite _disabledSprite;

    [SerializeField] private GameObject _cameraMenu;
    [SerializeField] private GameObject _cameraShop;

    private int _currentWindow; // 0 - main menu; 1 - shop; 2 - settings;

    private void Awake()
    {
        if (Instance != this && Instance != null)
            Destroy(this);
        else
            Instance = this;

        PlayerPrefs.SetInt(0 + "CarBought", 1);
    }
    private void Start()
    {
        PlayerPrefs.SetInt("NextLvl", 1);
        Initializate();    
    }
    private void Initializate()
    {
        UpdateMoney();
        UpdateSettings();

        _cameraMenu.SetActive(true);
        _cameraShop.SetActive(false);

        _canvasMain.gameObject.SetActive(false);
        _canvasSettings.gameObject.SetActive(false);
        _canvasShop.gameObject.SetActive(false);

        _canvasMain.alpha = 0f;
        _canvasSettings.alpha = 0f;
        _canvasShop.alpha = 0f;

        OpenMenu();
    }

    public void OpenShop()
    {
        _cameraMenu.SetActive(false);
        _cameraShop.SetActive(true);

        _currentWindow = 1;

        _canvasShop.gameObject.SetActive(true);

        _canvasMain.DOFade(0, _speedUITween).SetEase(Ease.OutCubic).SetLink(_canvasMain.gameObject);
        _canvasShop.DOFade(1, _speedUITween).SetEase(Ease.OutCubic).SetLink(_canvasShop.gameObject).OnKill(() =>
        {
            if (_currentWindow != 1)
                return;
            _canvasMain.gameObject.SetActive(false);
        });

        UpdateMoney();
        UpdateCarInfo(CarManager.Instance.CurrentCar());
    }
    public void OpenMenu()
    {
        _cameraMenu.SetActive(true);
        _cameraShop.SetActive(false);

        _currentWindow = 0;

        _canvasMain.gameObject.SetActive(true);

        _canvasSettings.DOFade(0, _speedUITween).SetEase(Ease.OutCubic).SetLink(_canvasShop.gameObject);
        _canvasShop.DOFade(0, _speedUITween).SetEase(Ease.OutCubic).SetLink(_canvasShop.gameObject);
        _canvasMain.DOFade(1, _speedUITween).SetEase(Ease.OutCubic).SetLink(_canvasMain.gameObject).OnKill(() =>
        {
            if (_currentWindow != 0)
                return;
            _canvasShop.gameObject.SetActive(false);
            _canvasSettings.gameObject.SetActive(false);
        });
    }
    public void OpenSettings()
    {
        UpdateSettings();

        _currentWindow = 2;

        _canvasSettings.gameObject.SetActive(true);

        _canvasMain.DOFade(0, _speedUITween).SetEase(Ease.OutCubic).SetLink(_canvasMain.gameObject);
        _canvasSettings.DOFade(1, _speedUITween).SetEase(Ease.OutCubic).SetLink(_canvasSettings.gameObject).OnKill(() =>
        {
            if (_currentWindow != 2)
                return;
            _canvasMain.gameObject.SetActive(false);
        });
    }
    public void RateUs()
    { 
        //open link
    }
    public void MoreGames()
    {
        //open link
    }
    public void Exit()
    {
        PlayerData.Instance.SaveData();
        Application.Quit();
    }
    public void DisableAd()
    {
        UpdateSettings();
    }
    public void ChangeArrow(int arrow, bool state)
    {
        _arrows[arrow].SetActive(state);
    }
    public void UpdateMoney()
    {
        float m = 0;
        DOTween.To(() => m, x => m = x, PlayerData.Instance.Money, 0.3f).OnUpdate(() => _money.text = Mathf.RoundToInt(m).ToString() + "$").SetLink(_money.gameObject);
    }
    public void UpdateCarInfo(Car car)
    {
        _carPrice.text = "PLAY";
        _carPrice.rectTransform.localScale = Vector3.zero;
        _carPrice.rectTransform.DOScale(new Vector3(1, 1, 1), 0.2f);

        _lock.SetActive(false);

        if (PlayerPrefs.GetInt(car.Id + "CarBought", 0) == 0)
        {
            float p = 0;
            DOTween.To(() => p, x => p = x, car.Price, 0.4f).OnUpdate(() => _carPrice.text = Mathf.RoundToInt(p).ToString() + "$").SetLink(_carPrice.gameObject);
            _lock.SetActive(true);
        }

        _speedBar.fillAmount = 0f;
        _brakeBar.fillAmount = 0f;
        _mobilityBar.fillAmount = 0f;

        _speedBar.DOFillAmount(car.Speed, 0.5f).SetLink(_speedBar.gameObject).SetEase(Ease.OutBack);
        _brakeBar.DOFillAmount(car.Brakes, 0.5f).SetLink(_brakeBar.gameObject).SetEase(Ease.OutBack);
        _mobilityBar.DOFillAmount(car.Mobility, 0.5f).SetLink(_mobilityBar.gameObject).SetEase(Ease.OutBack);
    }
    public void OnPlayPressed()
    {
        if (PlayerPrefs.GetInt(CarManager.Instance.CurrentCar().Id + "CarBought", 0) == 1)
        {
            PlayerData.Instance.SaveData();
            SceneChanger.Instance.LoadScene(PlayerPrefs.GetInt("NextLvl", 1));
        }
        else
        {
            CarManager.Instance.BuyCar();
        }
    }
    public void UpdateSettings()
    {
        _disableAd.interactable = true;

        if (PlayerPrefs.GetInt("AdEnabled", 1) == 0)
            _disableAd.interactable = false;

        if (AudioManager.Instance.IsAudioEnabled == true)
            _sound.sprite = _enabledSprite;
        else
            _sound.sprite = _disabledSprite;

        if (AudioManager.Instance.IsMusicEnabled == true)
            _music.sprite = _enabledSprite;
        else
            _music.sprite = _disabledSprite;
    }
}
