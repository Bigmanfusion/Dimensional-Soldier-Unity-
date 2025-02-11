using UnityEngine;

public class BulletBehavior : MonoBehaviour
{
    public float lifeTime = 2f; // Time before bullet gets destroyed

    void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        // Add logic for hitting enemies or objects
        if (collision.CompareTag("Enemy"))
        {
            Destroy(collision.gameObject); // Destroy enemy
            Destroy(gameObject); // Destroy bullet
        }
        else if (!collision.CompareTag("Player")) // Prevent bullet from destroying player
        {
            Destroy(gameObject); // Destroy bullet on impact with other objects
        }
    }
}
