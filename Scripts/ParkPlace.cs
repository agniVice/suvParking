using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParkPlace : MonoBehaviour
{
    [SerializeField] private List<GameObject> _carPrefabs;
    [SerializeField] private GameObject _currentCar;
    [SerializeField] private Transform _position;

    public void GenerateCar()
    {
        if (_currentCar != null)
            ClearPlace();


        _currentCar = Instantiate(_carPrefabs[Random.Range(0, _carPrefabs.Count)], _position.transform);
        _currentCar.transform.localPosition = Vector3.zero;
        _currentCar.transform.localRotation = Quaternion.Euler(new Vector3(0, GetRotation(), 0));

    }
    public void ClearPlace()
    {
        DestroyImmediate(_currentCar);
        _currentCar = null;
    }
    public float GetRotation()
    {
        if (Random.Range(0, 100) >= 50)
            return 180f;
        else
            return 0f;
    }
}
