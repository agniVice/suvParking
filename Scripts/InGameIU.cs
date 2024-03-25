using UnityEngine;
using TMPro;
using DG.Tweening;
using UnityEngine.UI;

public class InGameIU : MonoBehaviour
{
    public static InGameIU Instance;

    [SerializeField] private TextMeshProUGUI _crashes;
    [SerializeField] private TextMeshProUGUI _money;
    [SerializeField] private TextMeshProUGUI _parkText;

    [SerializeField] private TextMeshProUGUI _rewardText;
    [SerializeField] private TextMeshProUGUI _menuText;

    [SerializeField] private CanvasGroup _menuCanvas;
    [SerializeField] private CanvasGroup _mainCanvas;

    [SerializeField] private GameObject _btnResume;
    [SerializeField] private GameObject _btnNextLvl;
    [SerializeField] private GameObject _btnAd;

    [SerializeField] private Image _carDanger;

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
        _menuCanvas.alpha = 0f;
        _menuCanvas.gameObject.SetActive(false);

        _mainCanvas.alpha = 1f;
        _mainCanvas.gameObject.SetActive(true);

        _crashes.color = Color.green;

        OnMoneyChanged();
    }
    public void UpdateCrashes(int crashes, int maxCrashes)
    {
        _crashes.text = crashes + "/" + maxCrashes;
    }
    public void UpdateCrashesColor(Color color)
    {
        _crashes.color = color;
    }
    public void OnGameSuccess()
    {
        _btnResume.SetActive(false);
        _btnNextLvl.SetActive(true);

        _btnAd.SetActive(true);
        _menuText.text = "Level Success";

        _mainCanvas.DOFade(0, 0.7f).SetLink(_mainCanvas.gameObject).OnKill(() => { _mainCanvas.gameObject.SetActive(false); });

        _menuCanvas.gameObject.SetActive(true);
        _menuCanvas.DOFade(1, 0.7f).SetLink(_menuCanvas.gameObject).SetDelay(1f);

        float currentReward = 0;
        float reward = LvlData.Instance.RewardForSuccess();
        DOTween.To(() => currentReward, x => currentReward = x, reward, 1f).OnUpdate(() => _rewardText.text = "+" + Mathf.RoundToInt(currentReward).ToString() + "$").SetLink(_rewardText.gameObject).SetDelay(1f);

        PlayerData.Instance.OnLvlSuccess(LvlData.Instance.RewardForSuccess());

        PlayerData.Instance.SaveData();
    }
    public void OnGameFail()
    {
        _btnResume.SetActive(false);
        _btnNextLvl.SetActive(false);

        _btnAd.SetActive(false);
        _menuText.text = "Failed";

        _rewardText.gameObject.SetActive(false);

        _mainCanvas.DOFade(0, 0.7f).SetLink(_mainCanvas.gameObject).OnKill(() => { _mainCanvas.gameObject.SetActive(false); });

        _menuCanvas.gameObject.SetActive(true);
        _menuCanvas.DOFade(1, 0.7f).SetLink(_menuCanvas.gameObject);
    }
    public void OnCarParked(float currentTime, float maxTime)
    {
        _parkText.gameObject.SetActive(true);
        _parkText.text = currentTime.ToString("F1") + "s | " + maxTime + "s";
    }
    public void OnCarUnParked()
    {
        _parkText.gameObject.SetActive(false);
    }
    public void OnCarSuccessfullyParked()
    {
        _parkText.text = "SUCCESS!";
        _parkText.DOColor(new Color32(0, 0, 0, 0), 0.3f).SetLink(_parkText.gameObject).SetDelay(1f);
    }
    public void OnMoneyChanged()
    {
        float currentMoney = PlayerData.Instance.CurrentMoney;
        float money = PlayerData.Instance.Money;

        _money.DOColor(Color.green, 0.2f).SetLink(_money.gameObject);
        _money.DOColor(Color.white, 0.2f).SetLink(_money.gameObject).SetDelay(0.2f);

        DOTween.To(() => currentMoney, x => currentMoney = x, money, 1f).OnUpdate(() => _money.text = Mathf.RoundToInt(currentMoney).ToString() + "$").SetLink(_money.gameObject);
    }
    public void Double()
    {
        float currentReward = LvlData.Instance.RewardForSuccess();
        float reward = LvlData.Instance.RewardForSuccess() * 2;
        DOTween.To(() => currentReward, x => currentReward = x, reward, 1f).OnUpdate(() => _rewardText.text = "+" + Mathf.RoundToInt(currentReward).ToString() + "$").SetLink(_rewardText.gameObject).SetDelay(1f);
    }
    public void OnRewardDoubled()
    {
        RewardedAds.Instance.ShowAd();
    }
    public void PauseGame()
    {
        _btnResume.SetActive(true);
        _btnNextLvl.SetActive(false);

        _btnAd.SetActive(false);

        _menuText.text = "";
        _rewardText.text = "";

        _mainCanvas.gameObject.SetActive(false);
        _menuCanvas.gameObject.SetActive(true);
        _menuCanvas.alpha = 1f;

        GameState.Instance.Pause();
    }
    public void ResumeGame()
    {
        _mainCanvas.gameObject.SetActive(true);
        _menuCanvas.gameObject.SetActive(false);
        _menuCanvas.alpha = 0f;

        GameState.Instance.Resume();
    }
    public void CarDanger()
    {
        _carDanger.DOFade(0, 0.01f).SetLink(_carDanger.gameObject);
        _carDanger.gameObject.SetActive(true);
        _carDanger.DOFade(1, 0.3f).SetLink(_carDanger.gameObject).OnKill(() => {
            _carDanger.DOFade(0, 0.5f).SetLink(_carDanger.gameObject).SetDelay(1.5f).OnKill(() =>
            {
                _carDanger.gameObject.SetActive(false);
            });

        });
    }
}
