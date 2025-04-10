using UnityEngine;

public class Bomb : MonoBehaviour
{
    public float countdownTime = 3f;
    public Sprite bombSprite;
    public Sprite explosionSprite;
    public float explosionDuration = 0.2f;
    public int damage = 1;

    private bool hasExploded = false;
    private bool hasDamagedPlayer = false; // ✅ New flag
    private SpriteRenderer spriteRenderer;
    private CircleCollider2D circleCollider;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        circleCollider = GetComponent<CircleCollider2D>();
        spriteRenderer.sprite = bombSprite;

        circleCollider.enabled = false;
        Invoke(nameof(Explode), countdownTime);
    }

    void Explode()
    {
        if (hasExploded) return;

        hasExploded = true;
        spriteRenderer.sprite = explosionSprite;
        circleCollider.enabled = true;
        Invoke(nameof(DestroySelf), explosionDuration);
    }

    void DestroySelf()
    {
        Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // 💣 If not yet exploded, trigger explosion early
            if (!hasExploded)
            {
                CancelInvoke(nameof(Explode));
                Explode();
            }

            // 💥 Damage only once
            if (hasExploded && !hasDamagedPlayer)
            {
                hasDamagedPlayer = true;
                collision.GetComponent<PlayerHealth>()?.TakeDamage(damage);
            }
        }
    }
}
