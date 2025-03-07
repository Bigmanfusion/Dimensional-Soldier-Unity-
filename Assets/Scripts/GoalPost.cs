using UnityEngine;
using UnityEngine.SceneManagement;

public class GoalPost : MonoBehaviour
{
    public string nextSceneName = "VictoryScreen"; // Set this in the Inspector

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // Make sure your player has the "Player" tag
        {
            SceneManager.LoadScene(nextSceneName);
        }
    }
}
