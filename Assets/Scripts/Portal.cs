using UnityEngine;

public class Portal : MonoBehaviour
{
    public Portal linkedPortal;
    public float xOffset = 2f; // Offset for X-axis movement

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (linkedPortal != null && other.CompareTag("Player"))
        {
            Vector3 newPosition = other.transform.position;
            newPosition.x = linkedPortal.transform.position.x + xOffset; // Change only X-axis
                                                                         // Keep the Y position unchanged

            Rigidbody2D playerRb = other.GetComponent<Rigidbody2D>();
            playerRb.linearVelocity = Vector2.zero; // Reset velocity

            other.transform.position = newPosition; // Teleport player
        }
    }
}
