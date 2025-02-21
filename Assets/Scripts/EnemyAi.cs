using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public float moveSpeed = 2f;
    public float patrolDistance = 3f;
    public float detectionRange = 5f;
    public Transform player;
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float fireRate = 1f;

    private Vector2 startPos;
    private int direction = 1;
    private float nextFireTime;
    public float bulletSpeed = 10f;

    void Start()
    {
        startPos = transform.position;
    }

    void Update()
    {
        Patrol();
        DetectAndShoot();
    }

    void Patrol()
    {
        transform.Translate(Vector2.right * direction * moveSpeed * Time.deltaTime);

        if (Mathf.Abs(transform.position.x - startPos.x) >= patrolDistance)
        {
            direction *= -1;
            transform.localScale = new Vector3(direction, 1, 1);
        }
    }

    void DetectAndShoot()
    {
        if (player == null) return;

        float distanceToPlayer = Vector2.Distance(transform.position, player.position);
        if (distanceToPlayer <= detectionRange && Time.time >= nextFireTime)
        {
            Shoot();
            nextFireTime = Time.time + 1f / fireRate;
        }
    }

    void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
        Rigidbody2D bulletRb = bullet.GetComponent<Rigidbody2D>();
        float bulletDirection = (player.position.x > transform.position.x) ? 1f : -1f;
        bulletRb.linearVelocity = new Vector2(bulletSpeed * bulletDirection, 0f);
        bullet.tag = "EnemyBullet"; // Ensure bullets are tagged properly
    }
}
