using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player; // Assign the player in the inspector
    public float smoothSpeed = 5f; // Adjust for smoothness
    public Vector3 offset = new Vector3(0, 2, -10); // Adjust as needed

    void LateUpdate()
    {
        if (player != null)
        {
            Vector3 targetPosition = player.position + offset;
            transform.position = Vector3.Lerp(transform.position, targetPosition, smoothSpeed * Time.deltaTime);
        }
    }
}
