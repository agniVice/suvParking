using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CinemachineController : MonoBehaviour
{
    public static CinemachineController Instance;

    [SerializeField] private CinemachineVirtualCamera[] _vCams;
    [SerializeField] private CinemachineBrain _brain;
    private int _currentCam;

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
        _brain.m_DefaultBlend.m_Time = 0f;
    }
    public void SwitchCamera()
    {
        DisableAllCams();

        if (_currentCam + 1 >= _vCams.Length)
        {
            _currentCam = 0;
            _vCams[_currentCam].gameObject.SetActive(true);
        }
        else
        {
            _currentCam = _currentCam + 1;
            _vCams[_currentCam].gameObject.SetActive(true);
        }
    }
    private void DisableAllCams()
    {
        foreach(CinemachineVirtualCamera cam in _vCams)
            cam.gameObject.SetActive(false);
    }
    public void EnableMainCam()
    {
        DisableAllCams();
        _vCams[0].gameObject.SetActive(true);
    }
    public void EnableGameSuccessCam()
    {
        _brain.m_DefaultBlend.m_Time = 2f;

        DisableAllCams();

        _vCams[1].gameObject.SetActive(true);

        CinemachineComponentBase componentBase = _vCams[1].GetCinemachineComponent(CinemachineCore.Stage.Body);

        _vCams[1].m_Lens.FieldOfView = 10;
        if (componentBase is CinemachineFramingTransposer)
        {
            (componentBase as CinemachineFramingTransposer).m_CameraDistance = 30;
            (componentBase as CinemachineFramingTransposer).m_TrackedObjectOffset = new Vector3(0, 10, -2);
        }
    }
}
