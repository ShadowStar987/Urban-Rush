using UnityEngine;

public class Collectible : MonoBehaviour
{
    // How many points this collectible gives
    public int scoreValue = 10;

    void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the player touched this collectible
        if (other.CompareTag("Player"))
        {
            // Add the score to the GameManager
            GameManager.instance.AddScore(scoreValue);

            // Destroy this collectible so it disappears
            Destroy(gameObject);
        }
    }
}
