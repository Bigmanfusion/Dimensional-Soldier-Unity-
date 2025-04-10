using UnityEngine;

public class BulletBehavior : MonoBehaviour
{
    public float lifeTime = 2f;
    private bool isTeleporting = false; // Prevents infinite teleportation loop

    void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            collision.GetComponent<EnemyHealth>();
            Destroy(gameObject);
        }
        else if (collision.CompareTag("Player"))
        {
            collision.GetComponent<PlayerHealth>()?.TakeDamage(1);
            Destroy(gameObject);
        }
        else if (collision.CompareTag("Portal"))
        {
            if (!isTeleporting) // Prevents infinite teleportation loops
            {
                Portal portal = collision.GetComponent<Portal>();
                if (portal != null && portal.linkedPortal != null)
                {
                    TeleportThroughPortal(portal);
                }
            }
        }

        else if (!collision.CompareTag("Bullet"))
        {
            Destroy(gameObject);
        }
    }

    private void TeleportThroughPortal(Portal portal)
    {
        isTeleporting = true; // Prevent re-entering the portal immediately

        Transform linkedPortalTransform = portal.linkedPortal.transform;
        Vector2 exitDirection = portal.linkedPortal.transform.right; // Exit in portal's right direction
        float exitOffset = portal.exitOffset;

        // Move bullet to the linked portal position + small offset to prevent re-triggering
        transform.position = linkedPortalTransform.position + (Vector3)(exitDirection * exitOffset);

        // Maintain velocity and adjust bullet direction
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            float speed = rb.linearVelocity.magnitude; // Preserve speed
            rb.linearVelocity = exitDirection * speed; // Exit in new direction
        }

        isTeleporting = false;
    }
}
