using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    public static SceneChanger Instance;
    private int _id;

    private void Awake()
    {
        if (Instance != this && Instance != null)
            Destroy(this);
        else
            Instance = this;
    }
    public void LoadScene(int id = -1)
    {
        if (id != -1)
            _id = id;

        SceneManager.LoadScene(_id);
    }
    public void LoadSceneWithDelay(int id, float delay = 0f)
    {
        _id = id;
        Invoke("LoadScene", delay);
    }
}
