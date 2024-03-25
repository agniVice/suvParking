using UnityEngine;

public class PlayerData : MonoBehaviour
{
    public static PlayerData Instance;

    public float CurrentMoney = 0;
    public int Money;

    private void Awake()
    {
        if (Instance != this && Instance != null)
            Destroy(this);
        else
            Instance = this;

        Initializate();
    }
    private void Initializate()
    {
        Money = PlayerPrefs.GetInt("Money", 0);
        SaveData();
    }
    public void SaveData()
    {
        PlayerPrefs.SetInt("Money", Money);
    }
    public void OnCoinPickedUp()
    {
        CurrentMoney = Money;
        Money += LvlData.Instance.MoneyPerCoin();
    }
    public void OnLvlSuccess(int reward)
    {
        CurrentMoney = Money;
        Money += reward;
    }
}
