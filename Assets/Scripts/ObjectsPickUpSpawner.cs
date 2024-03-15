using UnityEngine;

public class ObjectsSpawner : MonoBehaviour
{
    public GameObject enemyPrefab; // Assign your enemy prefab in the inspector
    public float spawnRate = 2f; // How often to spawn enemies
    public Vector2 spawnAreaMin; // Minimum x and z values for spawning
    public Vector2 spawnAreaMax; // Maximum x and z values for spawning
    public float minSpawnTime = 1f; // Minimum time to spawn an enemy

    private float nextSpawnTime;

    void Update()
    {
        if (!PlayerController.Instance.gameHasStarted) return;

        if (Time.time < minSpawnTime) return;

        if (Time.time >= nextSpawnTime)
        {
            SpawnEnemy();
            nextSpawnTime = Time.time + spawnRate;
        }
    }

    void SpawnEnemy()
    {
        float spawnX = Random.Range(spawnAreaMin.x, spawnAreaMax.x);
        float spawnZ = Random.Range(spawnAreaMin.y, spawnAreaMax.y);

        Vector3 spawnPosition = new Vector3(spawnX, 0.5f, spawnZ); // Assuming the floor is at y = 0

        Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
    }
}
