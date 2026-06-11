using UnityEngine;
using System.Collections.Generic;

public class PlatformSpawner : MonoBehaviour
{
    [Header("Platform Settings")]
    public GameObject platformPrefab;      // The platform we will spawn
    public float platformWidth = 5f;       // How wide each platform is
    public float minGapSize = 2f;          // Minimum gap between platforms
    public float maxGapSize = 5f;          // Maximum gap between platforms
    public float minHeight = -2f;          // Lowest a platform can spawn
    public float maxHeight = 2f;           // Highest a platform can spawn

    [Header("Spawner Settings")]
    public Transform player;               // Reference to the player
    public float spawnAheadDistance = 20f; // How far ahead to spawn platforms
    public float deleteDistance = 20f;     // How far behind to delete platforms

    // List to keep track of all spawned platforms
    private List<GameObject> platforms = new List<GameObject>();
    private float nextSpawnX = 10f;        // Where the next platform will spawn

    void Update()
    {
        // Keep spawning platforms ahead of the player
        while (nextSpawnX < player.position.x + spawnAheadDistance)
        {
            SpawnPlatform();
        }

        // Delete platforms that are far behind the player
        for (int i = platforms.Count - 1; i >= 0; i--)
        {
            if (platforms[i] != null && platforms[i].transform.position.x < player.position.x - deleteDistance)
            {
                Destroy(platforms[i]);
                platforms.RemoveAt(i);
            }
        }
    }

    void SpawnPlatform()
    {
        // Pick a random height for the platform
        float randomHeight = Random.Range(minHeight, maxHeight);

        // Spawn the platform at the next position
        Vector3 spawnPosition = new Vector3(nextSpawnX, randomHeight, 0);
        GameObject newPlatform = Instantiate(platformPrefab, spawnPosition, Quaternion.identity);

        // Add it to our list so we can track it
        platforms.Add(newPlatform);

        // Calculate where the next platform will spawn
        float randomGap = Random.Range(minGapSize, maxGapSize);
        nextSpawnX += platformWidth + randomGap;
    }
}