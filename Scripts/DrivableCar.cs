using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class DrivableCar : MonoBehaviour
{
    private TriggerDrivableCar _trigger;
    [SerializeField] private Transform _positionToMove;
    [SerializeField] private float _speed = 1f;
    private void Awake()
    {
        _trigger = GetComponentInChildren<TriggerDrivableCar>();
        _trigger.Car = this;
    }
    public void Move()
    {
        transform.DOMove(_positionToMove.position, _speed).SetEase(Ease.OutCubic).SetLink(gameObject).OnKill(() => {
            transform.DOLocalMove(Vector3.zero, _speed * 0.5f).SetEase(Ease.OutCubic).SetLink(gameObject);
        });
        //AudioManager.Instance.PlaySound("CarDanger", 1f);
        InGameIU.Instance.CarDanger();
    }
}
