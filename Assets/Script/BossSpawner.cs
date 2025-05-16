using UnityEngine;

public class BossSpawner : MonoBehaviour
{
    public GameObject bossPrefab;
    public float spawnInterval = 300f; // 5 minutes
    private float timer = 0f;

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= spawnInterval)
        {
            SpawnBoss();
            timer = 0f;
        }
    }

    void SpawnBoss()
    {
        // Define spawn location (this could be random or a set point)
        Vector3 spawnPosition = new Vector3(0, 0, 10); // adjust as needed
        Instantiate(bossPrefab, spawnPosition, Quaternion.identity);
    }
}
