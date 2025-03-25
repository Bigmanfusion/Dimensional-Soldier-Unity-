using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public Transform pointA;
    public Transform pointB;
    public float speed = 2f;

    private Vector3 target;

    void Start()
    {
        if (pointA == null || pointB == null)
        {
            Debug.LogError("PointA or PointB is not assigned in " + gameObject.name);
            return;
        }

        target = pointB.position;
    }

    void Update()
    {
        if (pointA == null || pointB == null) return;

        transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);

        if (Vector3.Distance(transform.position, target) < 0.2f)
        {
            target = (target == pointA.position) ? pointB.position : pointA.position;
            Debug.Log("Platform switched direction to: " + target);
        }
 
            if (!gameObject.activeInHierarchy)
            {
                Debug.LogError("Platform is getting disabled!");
                gameObject.SetActive(true); // Reactivate if needed
            }
       



    }
}
