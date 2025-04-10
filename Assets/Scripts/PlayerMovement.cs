using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public float speed = 5f;
    public float jumpForce = 10f;
    public GameObject entrancePortalPrefab;
    public GameObject exitPortalPrefab;
    private GameObject entrancePortal, exitPortal;

    private Rigidbody2D rb;
    public GameObject pauseMenuUI;
    public static bool GameIsPaused = false;

    [Header("Shooting")]
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float bulletSpeed = 10f;

    // Variables for speed boost
    public float speedBoost = 900000f;
    private float originalSpeed;
    private bool isSpeedBoosted = false;
    private float speedBoostDuration = 20f;  // Duration of the speed boost

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        pauseMenuUI.SetActive(false);
        originalSpeed = speed;  // Store the original speed
    }

    void Update()
    {
        Move();
        Jump();
        HandlePortalPlacement();
        ShootBullet();
        HandlePause();
    }

    void Move()
    {
        float move = Input.GetAxis("Horizontal");
        float currentSpeed = speed;

        // If the player is boosted, increase the speed
        if (isSpeedBoosted)
        {
            currentSpeed *= speedBoost;
        }

        // Check if the run button (Left Shift) is held down
        if (Input.GetKey(KeyCode.LeftShift))
        {
            currentSpeed *= 1.5f; // Increase speed by 50%
        }

        rb.linearVelocity = new Vector2(move * currentSpeed, rb.linearVelocity.y); // Corrected from rb.linearVelocity to rb.velocity
    }

    void Jump()
    {
        if (Input.GetButtonDown("Jump") && Mathf.Abs(rb.linearVelocity.y) < 0.01f)  // Corrected from rb.linearVelocity to rb.velocity
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);  // Corrected from rb.linearVelocity to rb.velocity
        }
    }

    void HandlePortalPlacement()
    {
        if (Input.GetMouseButtonDown(0))
        {
            PlacePortal(ref entrancePortal, entrancePortalPrefab);
        }
        else if (Input.GetMouseButtonDown(1))
        {
            PlacePortal(ref exitPortal, exitPortalPrefab);
        }
    }

    void PlacePortal(ref GameObject portal, GameObject portalPrefab)
    {
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPos.z = 0;

        Collider2D hit = Physics2D.OverlapPoint(mouseWorldPos);
        if (hit != null) return;

        if (portal != null) Destroy(portal);

        portal = Instantiate(portalPrefab, mouseWorldPos, Quaternion.identity);

        if (entrancePortal != null && exitPortal != null)
        {
            Portal entrance = entrancePortal.GetComponent<Portal>();
            Portal exit = exitPortal.GetComponent<Portal>();

            entrance.linkedPortal = exit;
            exit.linkedPortal = entrance;
        }
    }

    void ShootBullet()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
            Rigidbody2D rbBullet = bullet.GetComponent<Rigidbody2D>();
            float direction = transform.localScale.x > 0 ? 1f : -1f;
            rbBullet.linearVelocity = new Vector2(direction * bulletSpeed, 0);  // Corrected from rb.linearVelocity to rb.velocity
        }
    }

    void HandlePause()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    // Trigger method to increase speed when touching SpeedAsher
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("SpeedDasher"))  // Make sure your SpeedAsher objects are tagged as "SpeedAsher"
        {
            isSpeedBoosted = true;
            Invoke("ResetSpeed", speedBoostDuration);  // Reset speed after a certain time
        }
    }

    // Reset the speed boost
    void ResetSpeed()
    {
        isSpeedBoosted = false;
    }
}
