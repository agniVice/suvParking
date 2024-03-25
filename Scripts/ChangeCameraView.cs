using UnityEngine;

public class ChangeCameraView : MonoBehaviour
{
    [SerializeField] private GameObject[] _cameras;
    private int _currentCam;

    private void Start()
    {
        foreach (GameObject go in _cameras)
            go.SetActive(false);

        _cameras[0].SetActive(true);
    }
    public void SwitchCamera()
    {
        foreach (GameObject go in _cameras)
            go.SetActive(false);

        int i = _currentCam + 1;

        if (i >= _cameras.Length)
        {
            _currentCam = 0;
            _cameras[_currentCam].SetActive(true);
        }
        else
        {
            _currentCam = i;
            _cameras[_currentCam].SetActive(true);
        }
    }
}
