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

            // Flip the fire point to match new direction
            firePoint.localPosition = new Vector3(Mathf.Abs(firePoint.localPosition.x) * direction, firePoint.localPosition.y, 0);
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

        // Fire in the enemy's moving direction
        bulletRb.linearVelocity = new Vector2(bulletSpeed * direction, 0f);
        bullet.tag = "EnemyBullet";
    }

}
