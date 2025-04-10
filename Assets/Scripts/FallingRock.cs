using UnityEngine;

public class RollingBoulderKillZone : MonoBehaviour
{
    public float speed = 5f; // Speed at which the boulder moves towards the player
    public float rotationSpeed = 100f; // Speed at which the boulder rotates (simulating rolling)
    private Transform player; // Reference to the player
    private Vector3 startPosition;

    private void Start()
    {
        // Find the player at the start
        player = GameObject.FindWithTag("Player").transform;
        if (player == null)
        {
            Debug.LogError("Player not found!");
        }

        startPosition = transform.position;
    }

    private void Update()
    {
        if (player != null)
        {
            // Move towards the player's position
            Vector3 direction = (player.position - transform.position).normalized;
            transform.position += direction * speed * Time.deltaTime;

            // Rotate the boulder to simulate rolling
            transform.Rotate(Vector3.forward * rotationSpeed * Time.deltaTime);

            // Optionally, you can limit the movement to the ground level or a certain axis if needed
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerController playerController = collision.GetComponent<PlayerController>();
            if (playerController != null)
            {
                playerController.RestartGame(); // Restart the level when the player dies
            }
        }
    }
}
