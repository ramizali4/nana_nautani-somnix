using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;   // Prefab of the enemy to spawn
    public float initialSpawnInterval = 2f; // Initial time interval between enemy spawns
    public float minSpawnInterval = 0.5f;     // Minimum time interval between spawns
    public float spawnOffset = 2f;   // Offset from the top of the screen where enemies will be spawned
    public float spawnIntervalDecreaseRate = 0.1f; // Rate at which spawn interval decreases over time

    private float currentSpawnInterval;

    private void Start()
    {
        currentSpawnInterval = initialSpawnInterval;
        InvokeRepeating("SpawnEnemy", 0f, currentSpawnInterval);
    }

    void SpawnEnemy()
    {
        // Calculate a random X position within the screen width
        float randomX = Random.Range(-Camera.main.orthographicSize * Camera.main.aspect, Camera.main.orthographicSize * Camera.main.aspect);

        // Calculate the spawn position at the top of the screen
        Vector3 spawnPosition = new Vector3(randomX, Camera.main.orthographicSize + spawnOffset, 0f);

        // Instantiate the enemy prefab at the calculated spawn position
        Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);

        // Decrease the spawn interval over time
        currentSpawnInterval = Mathf.Max(currentSpawnInterval - spawnIntervalDecreaseRate, minSpawnInterval);
    }
}
