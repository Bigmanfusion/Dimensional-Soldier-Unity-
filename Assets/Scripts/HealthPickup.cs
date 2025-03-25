using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    public int healthAmount = 1; // Amount of health restored

    void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerHealth playerHealth = collision.GetComponent<PlayerHealth>();

        if (playerHealth != null)
        {
            playerHealth.Heal(healthAmount);
            Destroy(gameObject); // Remove the pickup after use
        }
    }
}
