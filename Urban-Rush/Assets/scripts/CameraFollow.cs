using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;            // The player to follow
    public float smoothSpeed = 0.125f;  // How smooth the camera follows
    public Vector3 offset;             // Offset from player

    void LateUpdate()
    {
        // Move camera towards player smoothly
        Vector3 targetPosition = player.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, targetPosition, smoothSpeed);
        transform.position = smoothedPosition;
    }
}