using UnityEngine;

public class MovingKillZone : MonoBehaviour
{
    public float speed = 2f; // Speed of movement
    public float height = 3f; // How far it moves up and down
    private Vector3 startPosition;

    private void Start()
    {
        startPosition = transform.position;
    }

    private void Update()
    {
        float newY = startPosition.y + Mathf.Sin(Time.time * speed) * height;
        transform.position = new Vector3(startPosition.x, newY, startPosition.z);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerController player = collision.GetComponent<PlayerController>();
            if (player != null)
            {
                player.RestartGame(); // Restart the level when the player dies
            }
        }
    }
}
