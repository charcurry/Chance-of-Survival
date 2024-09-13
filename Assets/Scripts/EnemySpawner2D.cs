using UnityEngine;

public class EnemySpawner2D : MonoBehaviour
{
    public GameObject[] enemyPrefabs;  // The enemy prefab to spawn
    public float spawnDistance = 15f;  // Distance from the player to spawn enemies
    public float spawnInterval = 2f;  // Time interval between spawns
    public Vector2 xBounds = new Vector2(-20f, 20f); // X axis bounds for offscreen positions
    public Vector2 yBounds = new Vector2(-20f, 20f); // Y axis bounds for offscreen positions
    private PlayerMovement playerMovement;

    private Transform playerTransform;
    private float timer;

    void Start()
    {
        playerMovement = FindObjectOfType<PlayerMovement>();
        playerTransform = playerMovement.transform;
        timer = spawnInterval;
    }

    void Update()
    {
        timer -= Time.deltaTime;

        if (timer <= 0f)
        {
            SpawnEnemy();
            timer = spawnInterval;
        }
    }

    void SpawnEnemy()
    {
        if (enemyPrefabs.Length == 0)
        {
            Debug.LogWarning("No enemy prefabs assigned!");
            return;
        }

        GameObject enemyPrefab = enemyPrefabs[Random.Range(0, enemyPrefabs.Length)];

        Vector3 spawnPosition = GetRandomOffscreenPosition();
        Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
    }

    Vector3 GetRandomOffscreenPosition()
    {
        Vector3 playerPosition = playerTransform.position;

        // Choose a random direction to spawn from
        int direction = Random.Range(0, 4);
        Vector3 spawnPosition = playerPosition;

        switch (direction)
        {
            case 0: // Spawn to the left
                spawnPosition.x -= spawnDistance;
                spawnPosition.y = Random.Range(yBounds.x, yBounds.y);
                break;
            case 1: // Spawn to the right
                spawnPosition.x += spawnDistance;
                spawnPosition.y = Random.Range(yBounds.x, yBounds.y);
                break;
            case 2: // Spawn above
                spawnPosition.y += spawnDistance;
                spawnPosition.x = Random.Range(xBounds.x, xBounds.y);
                break;
            case 3: // Spawn below
                spawnPosition.y -= spawnDistance;
                spawnPosition.x = Random.Range(xBounds.x, xBounds.y);
                break;
        }

        return spawnPosition;
    }
}