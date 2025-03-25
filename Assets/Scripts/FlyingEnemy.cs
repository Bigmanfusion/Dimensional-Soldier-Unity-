using UnityEngine;

public class FlyingEnemy : MonoBehaviour
{
    public float speed = 3f;
    public Transform pointA;
    public Transform pointB;
    private Transform targetPoint;

    [Header("Bomb Settings")]
    public GameObject bombPrefab;
    public Transform bombDropPoint;
    public float dropInterval = 2f;
    private float dropTimer;

    void Start()
    {
        targetPoint = pointB;
        dropTimer = dropInterval;
    }

    void Update()
    {
        MoveBetweenPoints();
        DropBomb();
    }

    void MoveBetweenPoints()
    {
        transform.position = Vector2.MoveTowards(transform.position, targetPoint.position, speed * Time.deltaTime);

        if (Vector2.Distance(transform.position, targetPoint.position) < 0.1f)
        {
            targetPoint = targetPoint == pointA ? pointB : pointA;
        }
    }

    void DropBomb()
    {
        dropTimer -= Time.deltaTime;
        if (dropTimer <= 0)
        {
            Instantiate(bombPrefab, bombDropPoint.position, Quaternion.identity);
            dropTimer = dropInterval;
        }
    }
}
