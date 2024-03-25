using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarManager : MonoBehaviour
{
    public static CarManager Instance;

    public List<Car> Cars;
    public List<GameObject> CarModels;

    private int _currentCar;

    private void Awake()
    {
        if (Instance != this && Instance != null)
            Destroy(this);
        else
            Instance = this;
    }
    private void Start()
    {
        Initializate();
    }
    private void Initializate()
    {
        _currentCar = PlayerPrefs.GetInt("LastSelectedCar", 0);

        foreach (GameObject model in CarModels)
            model.SetActive(false);

        CarModels[_currentCar].SetActive(true);

        CheckAccess();
    }
    public void NextCar()
    {
        if (_currentCar < Cars.Count)
        {
            _currentCar++;

            SwitchCar();
        }
        CheckAccess();
    }
    public void PrevCar()
    {
        if (_currentCar > 0)
        {
            _currentCar--;

            SwitchCar();
        }
        CheckAccess();
    }
    private void SwitchCar()
    { 
        PlayerPrefs.SetInt("LastSelectedCar", _currentCar);
        MenuUI.Instance.UpdateCarInfo(Cars[_currentCar]);
        foreach (GameObject model in CarModels)
            model.SetActive(false);

        CarModels[_currentCar].SetActive(true);
    }
    public void BuyCar()
    {
        if (PlayerPrefs.GetInt(Cars[_currentCar].Id + "CarBought", 0) == 0)
        {
            if (PlayerData.Instance.Money >= Cars[_currentCar].Price)
            {
                PlayerPrefs.SetInt(Cars[_currentCar].Id + "CarBought", 1);

                PlayerData.Instance.Money -= Cars[_currentCar].Price;
                PlayerData.Instance.SaveData();
            }
        }
        MenuUI.Instance.UpdateCarInfo(Cars[_currentCar]);
    }
    public void Play()
    { 
        
    }
    public void CheckAccess()
    {
        MenuUI.Instance.ChangeArrow(0, true);
        MenuUI.Instance.ChangeArrow(1, true);

        if (_currentCar - 1 < 0)
            MenuUI.Instance.ChangeArrow(0, false);
        if(_currentCar + 1 == Cars.Count)
            MenuUI.Instance.ChangeArrow(1, false);
    }
    public Car CurrentCar()
    {
        return Cars[_currentCar];
    }
}
