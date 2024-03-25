using UnityEngine;

public class GameState : MonoBehaviour
{
    public static GameState Instance;

    public bool IsGameOver = false;
    public bool IsGamePaused;

    private void Awake()
    {
        if (Instance != this && Instance != null)
            Destroy(this);
        else
            Instance = this;
    }
    public void Success()
    {
        Time.timeScale = 1f;
        IsGameOver = true;
        InGameIU.Instance.OnGameSuccess();
        CinemachineController.Instance.EnableGameSuccessCam();
        GeneralCarController.Instance.CarController.enabled = false;
        if (LvlData.Instance.Id() != 6)
            PlayerPrefs.SetInt("NextLvl", LvlData.Instance.Id()+1);
        else
            PlayerPrefs.SetInt("NextLvl", 0);

        PlayerData.Instance.SaveData();
    }
    public void Fail()
    {
        Time.timeScale = 1f;
        InGameIU.Instance.OnGameFail();
        GeneralCarController.Instance.CarController.enabled = false;
    }
    public void RestartLvl()
    {
        Time.timeScale = 1f;
        SceneChanger.Instance.LoadScene(LvlData.Instance.Id());
    }
    public void NextLvl()
    {
        Time.timeScale = 1f;
        SceneChanger.Instance.LoadScene(PlayerPrefs.GetInt("NextLvl", 1));
    }
    public void Home()
    {
        Time.timeScale = 1f;
        SceneChanger.Instance.LoadScene(0);
    }
    public void Resume()
    {
        IsGamePaused = true;
        Time.timeScale = 1f;
    }
    public void Pause()
    {
        IsGamePaused = false;
        Time.timeScale = 0f;
    }
}
