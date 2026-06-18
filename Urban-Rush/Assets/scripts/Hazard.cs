using UnityEngine;

public class Hazard : MonoBehaviour
{
    // When something touches this hazard
    void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the thing that touched it is the player
        if (other.CompareTag("Player"))
        {
            // Tell the GameManager the player lost a life
            // (the GameManager handles respawning and game over)
            GameManager.instance.LoseLife();
        }
    }
}