using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    public int healthToAdd = 10;

    private void Update()
    { 
        transform.Rotate(0,0,60*Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Player player = other.gameObject.GetComponent<Player>();
            if (player != null && !player.IsHealthFull())
            {
                player.Heal(healthToAdd);
                Destroy(gameObject);
            }
        }
    }
}
