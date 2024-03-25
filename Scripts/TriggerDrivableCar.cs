using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerDrivableCar : MonoBehaviour
{
    public DrivableCar Car;
    private BoxCollider _collider;

    private void Awake()
    {
        _collider = GetComponent<BoxCollider>();
    }

    public void OnCarTriggered()
    {
        Car.Move();
        _collider.enabled = false;
    }
}
