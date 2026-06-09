using UnityEngine;

public class GameManager : MonoBehaviour
{
    // The one instance of the GameManager (accessible from anywhere)
    public static GameManager instance;

    // The position where the player respawns when they die
    public Vector3 respawnPoint;

    // The player's current score
    public int score = 0;

    void Awake()
    {
        // Make sure there is only one GameManager at all times
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Call this to add points to the score
    public void AddScore(int amount)
    {
        score += amount;
        Debug.Log("Score: " + score);
    }
}