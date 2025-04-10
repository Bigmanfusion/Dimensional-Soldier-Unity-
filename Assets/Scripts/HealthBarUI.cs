using UnityEngine;
using UnityEngine.UI;

public class HealthBarUI : MonoBehaviour
{
    public Slider healthSlider;
    public PlayerHealth playerHealth;

    void Start()
    {
        healthSlider.maxValue = playerHealth.maxHealth;
        healthSlider.value = playerHealth.maxHealth;
    }

    void Update()
    {
        healthSlider.value = playerHealth.GetCurrentHealth();
    }
}
