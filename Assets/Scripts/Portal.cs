using UnityEngine;
using System.Collections;

public class Portal : MonoBehaviour
{
    public Portal linkedPortal;
    public float exitOffset = 0.5f;
    public float collisionDisableTime = 0.1f;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (linkedPortal == null) return;

        if (other.CompareTag("Player"))
        {
            TeleportObject(other.gameObject);
        }
        else if (other.CompareTag("Bullet"))
        {
            BulletBehavior bullet = other.GetComponent<BulletBehavior>();
            if (bullet != null)
            {
                bullet.SendMessage("TeleportThroughPortal", this);
            }
        }
    }

    private void TeleportObject(GameObject obj)
    {
        Rigidbody2D rb = obj.GetComponent<Rigidbody2D>();
        Collider2D objCollider = obj.GetComponent<Collider2D>();

        if (rb != null && objCollider != null)
        {
            Vector2 velocity = rb.linearVelocity;
            Vector2 exitDirection = linkedPortal.transform.right;
            Vector3 newPosition = linkedPortal.transform.position + (Vector3)(exitDirection * exitOffset);

            obj.transform.position = newPosition;
            rb.linearVelocity = velocity;
            StartCoroutine(DisableCollisionsTemporarily(objCollider));
        }
    }

    private IEnumerator DisableCollisionsTemporarily(Collider2D objCollider)
    {
        Collider2D portalCollider = GetComponent<Collider2D>();
        Collider2D linkedPortalCollider = linkedPortal.GetComponent<Collider2D>();

        Physics2D.IgnoreCollision(objCollider, portalCollider, true);
        Physics2D.IgnoreCollision(objCollider, linkedPortalCollider, true);

        yield return new WaitForSeconds(collisionDisableTime);

        Physics2D.IgnoreCollision(objCollider, portalCollider, false);
        Physics2D.IgnoreCollision(objCollider, linkedPortalCollider, false);
    }
}
