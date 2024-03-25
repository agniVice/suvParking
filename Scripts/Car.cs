using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Car", menuName = "Cars/New car")]
public class Car : ScriptableObject
{
    public int Price;

    public int Id;

    [Range(0,1)]
    public float Speed;
    [Range(0, 1)]
    public float Brakes;
    [Range(0, 1)]
    public float Mobility;
}
