using UnityEngine;
using System.Collections.Generic;

public class PlatformSpawner : MonoBehaviour
{
    [Header("Platform Settings")]
    public GameObject platformPrefab;      // The platform we will spawn
    public float platformWidth = 5f;       // How wide each platform is
    public float minGapSize = 1f;          // Minimum gap between platforms
    public float maxGapSize = 2.5f;        // Maximum gap between platforms
    public float minHeight = -2f;          // Lowest a platform can spawn
    public float maxHeight = 2f;           // Highest a platform can spawn

    [Header("Spray Can Settings")]
    public GameObject sprayCanPrefab;      // The spray can we collect
    [Range(0f, 1f)]
    public float sprayCanChance = 0.5f;    // Chance (0-1) a platform has a can
    public float sprayCanHeight = 1.2f;    // How high above the platform the can sits

    [Header("Spike Settings")]
    public GameObject spikePrefab;         // The spike that hurts the player
    [Range(0f, 1f)]
    public float spikeChance = 0.3f;       // Chance (0-1) a platform has a spike
    public float spikeHeight = 0.6f;       // How high above the platform center the spike sits

    [Header("Spawner Settings")]
    public Transform player;               // Reference to the player
    public float spawnAheadDistance = 20f; // How far ahead to spawn platforms
    public float deleteDistance = 20f;     // How far behind to delete platforms

    public float firstPlatformX = 2f;      // Where the FIRST platform spawns
    private float nextSpawnX;               // Tracks where the next platform will spawn

    // List to keep track of all spawned objects
    private List<GameObject> platforms = new List<GameObject>();

    void Start()
    {
        // Set the starting spawn position from the Inspector value
        nextSpawnX = firstPlatformX;
    }

    void Update()
    {
        // Keep spawning platforms ahead of the player
        while (nextSpawnX < player.position.x + spawnAheadDistance)
        {
            SpawnPlatform();
        }

        // Delete objects that are far behind the player
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

        // Decide what goes on this platform: spike OR spray can (not both)
        if (Random.value <= spikeChance)
        {
            TrySpawnSpike(spawnPosition);
        }
        else
        {
            TrySpawnSprayCan(spawnPosition);
        }

        // Calculate where the next platform will spawn
        float randomGap = Random.Range(minGapSize, maxGapSize);
        nextSpawnX += platformWidth + randomGap;
    }

    void TrySpawnSprayCan(Vector3 platformPosition)
    {
        // Stop if no spray can prefab was assigned
        if (sprayCanPrefab == null) return;

        // Roll a random number; only spawn if it's under our chance
        if (Random.value <= sprayCanChance)
        {
            Vector3 canPosition = platformPosition + new Vector3(0, sprayCanHeight, 0);
            GameObject can = Instantiate(sprayCanPrefab, canPosition, Quaternion.identity);
            platforms.Add(can);
        }
    }

    void TrySpawnSpike(Vector3 platformPosition)
    {
        // Stop if no spike prefab was assigned
        if (spikePrefab == null) return;

        // Place the spike on top of the platform
        Vector3 spikePosition = platformPosition + new Vector3(0, spikeHeight, 0);
        GameObject spike = Instantiate(spikePrefab, spikePosition, Quaternion.identity);
        platforms.Add(spike);
    }
}