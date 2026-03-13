using UnityEngine;

public class RoomTrigger : MonoBehaviour
{
    public GameObject objectToEnable;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            objectToEnable.SetActive(true);
        }
    }
}
