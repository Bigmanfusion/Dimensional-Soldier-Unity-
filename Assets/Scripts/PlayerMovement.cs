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

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        pauseMenuUI.SetActive(false);
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
        rb.linearVelocity = new Vector2(move * speed, rb.linearVelocity.y);
    }

    void Jump()
    {
        if (Input.GetButtonDown("Jump") && Mathf.Abs(rb.linearVelocity.y) < 0.01f)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
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
            rbBullet.linearVelocity = new Vector2(direction * bulletSpeed, 0);
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
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}


