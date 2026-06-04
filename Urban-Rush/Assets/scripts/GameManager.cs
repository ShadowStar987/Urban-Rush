using UnityEngine;

public class GameManager : MonoBehaviour
{
    // The one instance of the GameManager (accessible from anywhere)
    public static GameManager instance;

    // The position where the player respawns when they die
    public Vector3 respawnPoint;

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
}