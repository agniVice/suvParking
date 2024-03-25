using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.UI;

public class CarSpawner : MonoBehaviour
{
    [SerializeField] private List<GameObject> _cars;
    [SerializeField] private Transform _spawnPosition;
    [SerializeField] private GeneralCarController _carController;
    [SerializeField] private CarSetting _carSetting;

    [SerializeField] private CinemachineVirtualCamera _vCam1;
    [SerializeField] private CinemachineVirtualCamera _vCam2;
    [SerializeField] private CinemachineVirtualCamera _vCam3;

    [SerializeField] private GameObject ThrottleButton;
    [SerializeField] private GameObject BrakesButton;
    [SerializeField] private GameObject TurnLeftButton;
    [SerializeField] private GameObject TurnRightButton;
    [SerializeField] private GameObject HandBrakeButton;

    [SerializeField] private Text SpeedText;


    private void Start()
    {
        SpawnCar();    
    }
    private void SpawnCar()
    {
        GameObject car = Instantiate(_cars[PlayerPrefs.GetInt("LastSelectedCar", 0)], null);
        car.transform.position = _spawnPosition.position;
        car.transform.rotation = _spawnPosition.rotation;

        _carSetting = FindObjectOfType<CarSetting>();

        _carController.CarController = _carSetting.PrometeoController;
        _carController.ColliderChecker = _carSetting.CheckerCollider;
        _carController.SteeringWheel = _carSetting.Steering;
        _carController.GearBox = _carSetting.Shifter;

        _vCam1.Follow = car.transform;
        _vCam1.LookAt = car.transform;

        _vCam2.Follow = car.transform;
        _vCam2.LookAt = car.transform;

        _vCam3.Follow = _carSetting.BackCameraFollow.transform;
        _vCam3.LookAt = _carSetting.BackCameraFollow.transform;

        AudioManager.Instance.AddSound(_carSetting.EngineSound);
        AudioManager.Instance.AddSound(_carSetting.WheelsSound);

        _carController.CarController.throttleButton = ThrottleButton;
        _carController.CarController.reverseButton = BrakesButton;
        _carController.CarController.turnLeftButton = TurnLeftButton;
        _carController.CarController.turnRightButton = TurnRightButton;
        _carController.CarController.handbrakeButton = HandBrakeButton;

        _carController.CarController.carSpeedText = SpeedText;
    }
}
