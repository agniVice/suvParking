using UnityEngine;

public class ColliderChecker : MonoBehaviour
{
    [SerializeField] private MeshRenderer _carMesh;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag != "UnCountedCrash")
        {
            GeneralCarController.Instance.OnCarCrashed();
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Coin")
        {
            AudioManager.Instance.PlaySound("CoinPickup", 0.7f);
            PlayerData.Instance.OnCoinPickedUp();
            InGameIU.Instance.OnMoneyChanged();

            other.GetComponent<BoxCollider>().enabled = false;

            Destroy(other.gameObject);
        }
        if (other.gameObject.tag == "TriggerDrivableCar")
        {
            other.GetComponent<TriggerDrivableCar>().OnCarTriggered();
        }
    }
    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.name == "Park")
        {
            if (other.bounds.Contains(_carMesh.bounds.min) && other.bounds.Contains(_carMesh.bounds.max))
                ParkZone.Instance.IsCarInParkZone = true;
            else
                ParkZone.Instance.IsCarInParkZone = false;
        }
    }
}
