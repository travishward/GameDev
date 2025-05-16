using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour
{
    // The enemy prefab to spawn (assign this in the Inspector)
    public GameObject enemyPrefab;

    // How often (in seconds) to spawn an enemy
    public float spawnInterval = 5f;

    // Define the spawn area dimensions (the area is a rectangle on the XZ plane)
    public float spawnAreaWidth = 50f;   // Total width along the X axis
    public float spawnAreaDepth = 50f;   // Total depth along the Z axis

    // The Y coordinate where enemies will spawn (usually ground level, e.g., 0)
    public float spawnY = 0f;

    void Start()
    {
        // Start a repeating routine to spawn enemies
        StartCoroutine(SpawnEnemyRoutine());
    }

    IEnumerator SpawnEnemyRoutine()
    {
        while (true)
        {
            SpawnEnemy();
            // Wait for spawnInterval seconds before spawning the next enemy
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    void SpawnEnemy()
    {
        // Calculate a random position within the defined area
        // The area is centered at (0, spawnY, 0)
        float randomX = Random.Range(-spawnAreaWidth / 2, spawnAreaWidth / 2);
        float randomZ = Random.Range(-spawnAreaDepth / 2, spawnAreaDepth / 2);
        Vector3 spawnPosition = new Vector3(randomX, spawnY, randomZ);

        // Instantiate the enemy prefab at the random position with no rotation.
        Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
    }
}
