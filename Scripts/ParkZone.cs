using UnityEngine;

public class ParkZone : MonoBehaviour
{
    public static ParkZone Instance;

    public bool IsCarInParkZone;

    [SerializeField] private Material _materialParkLine;
    [SerializeField] private ParticleSystem _particleSystem;

    [SerializeField] private int _maxTime;
    private float _currentTime = 0f;

    private BoxCollider _collider;

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
    private void FixedUpdate()
    {
        if (GameState.Instance.IsGameOver)
            return;
        if (!IsCarInParkZone)
        {
            OnCarUnParked();
            return;
        }
        if (_currentTime >= _maxTime)
        {
            OnCarSuccessfullyParked();
            GameState.Instance.Success();
            return;
        }
        else
            _currentTime += Time.fixedDeltaTime;

        OnCarParked(_currentTime, _maxTime);

    }
    private void Initialization()
    {
        _collider = GetComponent<BoxCollider>();

        _collider.enabled = true;

        _materialParkLine.color = Color.yellow;
        _particleSystem.startColor = Color.yellow;
    }
    public void OnCarParked(float currentTime, float maxTime)
    {
        _materialParkLine.color = Color.green;
        _particleSystem.startColor = Color.green;

        InGameIU.Instance.OnCarParked(currentTime, maxTime);
    }
    public void OnCarUnParked()
    {
        _materialParkLine.color = Color.yellow;
        _particleSystem.startColor = Color.yellow;

        _currentTime = 0f;

        InGameIU.Instance.OnCarUnParked();
    }
    private void OnCarSuccessfullyParked()
    {
        _collider.enabled = false;

        InGameIU.Instance.OnCarSuccessfullyParked();
    }
}
