using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarSetting : MonoBehaviour
{
    public PrometeoCarController PrometeoController;
    public SteeringWheel Steering;
    public ColliderChecker CheckerCollider;
    public GearBox Shifter;
    public GameObject BackCameraFollow;

    public AudioSource EngineSound;
    public AudioSource WheelsSound;
}
