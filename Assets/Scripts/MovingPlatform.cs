using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public Vector2 startPoint;
    public Vector2 endPoint;
    public float speed = 2f;
    private bool movingToEnd = true;

    void Update()
    {
        MovePlatform();
    }

    void MovePlatform()
    {
        if (movingToEnd)
        {
            transform.position = Vector2.MoveTowards(transform.position, endPoint, speed * Time.deltaTime);
            if ((Vector2)transform.position == endPoint)
            {
                movingToEnd = false;
            }
        }
        else
        {
            transform.position = Vector2.MoveTowards(transform.position, startPoint, speed * Time.deltaTime);
            if ((Vector2)transform.position == startPoint)
            {
                movingToEnd = true;
            }
        }
    }
}
