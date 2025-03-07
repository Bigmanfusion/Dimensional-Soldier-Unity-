using UnityEngine;

public class Portal : MonoBehaviour
{
    public Portal linkedPortal; // Reference to the other portal
    public float exitOffset = 1f; // Distance offset when teleporting
    public float collisionDisableTime = 2f; // Time to disable collisions

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (linkedPortal != null && other.CompareTag("Player"))
        {
            Rigidbody2D playerRb = other.GetComponent<Rigidbody2D>();
            Collider2D playerCollider = other.GetComponent<Collider2D>();

            if (playerRb != null && playerCollider != null)
            {
                // Preserve player's velocity while teleporting
                Vector2 velocity = playerRb.linearVelocity;

                // Determine exit direction based on player's movement
                Vector3 exitDirection = linkedPortal.transform.right;
                if (velocity.x < 0) // Adjust direction based on movement
                {
                    exitDirection = -linkedPortal.transform.right;
                }

                // Teleport the player to the linked portal with an offset
                Vector3 newPosition = linkedPortal.transform.position + (Vector3)(exitDirection * exitOffset);
                other.transform.position = newPosition;

                // Maintain momentum when exiting the portal
                playerRb.linearVelocity = velocity;

                // Temporarily disable collisions with portals
                StartCoroutine(DisableCollisionsTemporarily(playerCollider));
            }
        }
    }

    private System.Collections.IEnumerator DisableCollisionsTemporarily(Collider2D playerCollider)
    {
        Collider2D portalCollider = GetComponent<Collider2D>();
        Collider2D linkedPortalCollider = linkedPortal.GetComponent<Collider2D>();

        Physics2D.IgnoreCollision(playerCollider, portalCollider, true);
        Physics2D.IgnoreCollision(playerCollider, linkedPortalCollider, true);

        yield return new WaitForSeconds(collisionDisableTime);

        Physics2D.IgnoreCollision(playerCollider, portalCollider, false);
        Physics2D.IgnoreCollision(playerCollider, linkedPortalCollider, false);
    }
}
