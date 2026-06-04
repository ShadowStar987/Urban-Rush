using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    // --- Settings you can change in the Inspector ---
    public Transform player;            // The player object to follow
    public float smoothSpeed = 0.125f;  // How smoothly the camera follows (lower = smoother)
    public Vector3 offset;             // Distance between camera and player

    void LateUpdate()
    {
        // The position we want the camera to move towards
        Vector3 targetPosition = player.position + offset;

        // Smoothly move the camera towards the target position
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, targetPosition, smoothSpeed);

        // Apply the new position to the camera
        transform.position = smoothedPosition;
    }
}