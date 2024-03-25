using UnityEngine;

public class LvlData : MonoBehaviour
{
    public static LvlData Instance;

    [SerializeField] private int _id;
    [SerializeField] private int _moneyPerCoin = 1;
    [SerializeField] private int _rewardForSuccess;

    private void Awake()
    {
        if (Instance != this && Instance != null)
            Destroy(this);
        else
            Instance = this;
    }
    public int MoneyPerCoin()
    {
        return _moneyPerCoin;
    }
    public int RewardForSuccess()
    {
        return _rewardForSuccess;
    }
    public int Id()
    {
        return _id;
    }
}
