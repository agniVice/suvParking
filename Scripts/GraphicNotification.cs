using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GraphicNotification : MonoBehaviour
{
    [SerializeField] private GameObject _panel;

    private void Start()
    {
        _panel.SetActive(true);
    }
    public void Ok()
    {
        _panel.SetActive(false);
    }
}
