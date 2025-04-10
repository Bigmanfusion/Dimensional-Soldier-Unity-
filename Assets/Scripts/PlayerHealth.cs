using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 30;
    private int currentHealth;

    void Start()
    {
        currentHealth = maxHealth;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("EnemyBullet"))
        {
            TakeDamage(1);
            Destroy(collision.gameObject); // Destroy the bullet on hit
            Debug.Log("You've been hit");
        }

        if (collision.CompareTag("EnemyBomb"))
        {
            TakeDamage(1);
            Destroy(collision.gameObject); // Destroy the bullet on hit
            Debug.Log("You've been hit");
        }
    }

    public void Heal(int amount)
    {
        currentHealth = Mathf.Min(currentHealth + amount, maxHealth);
        Debug.Log("Player healed! Current Health: " + currentHealth);
    }



    public void TakeDamage(int damage) // Make this method public
    {
        currentHealth -= damage;
        Debug.Log("Player Health: " + currentHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log("Player has died!");
        SceneManager.LoadScene("MainMenu"); // Load the Main Menu scene
    }

    public int GetCurrentHealth()
    {
        return currentHealth;
    }

}
