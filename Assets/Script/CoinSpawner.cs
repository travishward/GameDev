using UnityEngine;

public class CoinSpawner : MonoBehaviour
{
    public GameObject coinPrefab;           // The coin prefab to spawn (assign via Inspector)
    public int numberOfCoinsToSpawn = 10;     // Total coins to spawn during the event
    public Vector3 spawnAreaMin;              // Minimum bounds (x, y, z) of the spawn area
    public Vector3 spawnAreaMax;              // Maximum bounds (x, y, z) of the spawn area

    // Call this method to trigger the coin event
    public void SpawnCoins()
    {
        for (int i = 0; i < numberOfCoinsToSpawn; i++)
        {
            // Generate random coordinates within the defined area
            float x = Random.Range(spawnAreaMin.x, spawnAreaMax.x);
            float y = Random.Range(spawnAreaMin.y, spawnAreaMax.y);
            float z = Random.Range(spawnAreaMin.z, spawnAreaMax.z);
            Vector3 spawnPosition = new Vector3(x, y, z);

            // Instantiate the coin prefab at the random position
            Instantiate(coinPrefab, spawnPosition, Quaternion.identity);
        }
    }
}
