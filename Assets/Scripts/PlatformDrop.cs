using UnityEngine;

public class FallingPlatform : MonoBehaviour
{
    public float dropDelay = 1.5f; // Time before the platform falls
    public float respawnTime = 3f; // Time before the platform reappears
    private Rigidbody2D rb;
    private Vector3 initialPosition;
    private bool isFalling = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.isKinematic = true; // Prevent movement until triggered
        initialPosition = transform.position;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !isFalling)
        {
            isFalling = true;
            Invoke("DropPlatform", dropDelay);
        }
    }

    void DropPlatform()
    {
        rb.isKinematic = false;
        Invoke("ResetPlatform", respawnTime);
    }

    void ResetPlatform()
    {
        rb.isKinematic = true;
        rb.linearVelocity = Vector2.zero;
        rb.angularVelocity = 0f;
        transform.position = initialPosition;
        isFalling = false;
    }
}
