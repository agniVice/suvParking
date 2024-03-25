using UnityEngine;

public class GeneralCarController : MonoBehaviour
{
    public static GeneralCarController Instance;

    public PrometeoCarController CarController;
    public ColliderChecker ColliderChecker;
    public SteeringWheel SteeringWheel;
    public GearBox GearBox;

    [SerializeField] private int _maxCrashes;

    private int _crashes;

    private void Awake()
    {
        if (Instance != this && Instance != null)
            Destroy(this);
        else
            Instance = this;
    }
    public void OnCarCrashed()
    {
        _crashes++;
        if (_crashes > _maxCrashes)
            GameState.Instance.Fail();

        switch (_crashes)
        {
            case 1:
                InGameIU.Instance.UpdateCrashesColor(Color.yellow);
                break;
            case 2:
                InGameIU.Instance.UpdateCrashesColor(new Color32(255, 147, 47, 255));
                break;
            case 3:
                InGameIU.Instance.UpdateCrashesColor(Color.red);
                break;
        }

        InGameIU.Instance.UpdateCrashes(_crashes, _maxCrashes);

        CheckCrashForce();
    }
    private void CheckCrashForce()
    {
        if (CarController.carRigidbody.velocity.magnitude < 1)
            AudioManager.Instance.PlaySound("CarCrash", 0.3f);
        else if (CarController.carRigidbody.velocity.magnitude > 1 && CarController.carRigidbody.velocity.magnitude < 3)
            AudioManager.Instance.PlaySound("CarCrash", 0.7f);
        else if (CarController.carRigidbody.velocity.magnitude > 3)
            AudioManager.Instance.PlaySound("CarCrash", 1f);
    }
}
