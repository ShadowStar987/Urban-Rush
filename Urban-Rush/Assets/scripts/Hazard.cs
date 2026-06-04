using UnityEngine;

public class Hazard : MonoBehaviour
{
    // When something touches this hazard
    void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the thing that touched it is the player
        if (other.CompareTag("Player"))
        {
            // Respawn the player at the start position
            other.transform.position = GameManager.instance.respawnPoint;
        }
    }
}
