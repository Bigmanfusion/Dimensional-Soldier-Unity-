using UnityEngine;

public class BulletBehavior : MonoBehaviour
{
    public float lifeTime = 2f;

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
        else if (!collision.CompareTag("Bullet"))
        {
            Destroy(gameObject);
        }
    }
}
