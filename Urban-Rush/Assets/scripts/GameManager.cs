using UnityEngine;
using UnityEngine.SceneManagement; // Lets us reload the scene to restart
using TMPro; // Lets us use TextMeshPro text in code

public class GameManager : MonoBehaviour
{
    // The one instance of the GameManager (accessible from anywhere)
    public static GameManager instance;

    // The position where the player respawns when they die
    public Vector3 respawnPoint;

    // The player's current score
    public int score = 0;

    // How many lives the player starts with
    public int startingLives = 3;

    // The player's current lives (set in Start)
    private int currentLives;

    // Drag the ScoreText object here in the Inspector
    public TextMeshProUGUI scoreText;

    // Drag the LivesText object here in the Inspector
    public TextMeshProUGUI livesText;

    // Drag the GameOverPanel object here in the Inspector
    public GameObject gameOverPanel;

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

    void Start()
    {
        // Set lives to the starting amount when the game begins
        currentLives = startingLives;

        // Show the starting score and lives on screen
        UpdateScoreText();
        UpdateLivesText();

        // Make sure the game over panel is hidden when the game starts
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(false);
        }
    }

    // Call this to add points to the score
    public void AddScore(int amount)
    {
        score += amount;
        UpdateScoreText(); // Refresh the text every time score changes
    }

    // Call this when the player should lose a life
    public void LoseLife()
    {
        // Take one life away
        currentLives--;

        // Update the on-screen lives number
        UpdateLivesText();

        // If there are still lives left, respawn the player
        if (currentLives > 0)
        {
            RespawnPlayer();
        }
        else
        {
            // No lives left - show the game over screen
            GameOver();
        }
    }

    // Moves the player back to the respawn point
    void RespawnPlayer()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            player.transform.position = respawnPoint;
        }
    }

    // Shows the game over screen and freezes the game
    void GameOver()
    {
        // Turn on the game over panel so the player sees it
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(true);
        }

        // Freeze the game by stopping time
        Time.timeScale = 0f;
    }

    // Called by the Restart button to start over
    public void RestartGame()
    {
        // Unfreeze time before reloading
        Time.timeScale = 1f;

        // Reload the current scene from the start
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    // Writes the current score into the UI text
    void UpdateScoreText()
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: " + score;
        }
    }

    // Writes the current lives into the UI text
    void UpdateLivesText()
    {
        if (livesText != null)
        {
            livesText.text = "Lives: " + currentLives;
        }
    }
}