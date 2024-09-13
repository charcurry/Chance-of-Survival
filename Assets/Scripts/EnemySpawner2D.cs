using UnityEngine;

public class EnemySpawner2D : MonoBehaviour
{
    public GameObject[] enemyPrefabs;    // Array of enemy prefabs to spawn
    public float spawnDistance = 15f;    // Distance from the player to spawn enemies
    public float spawnInterval = 2f;     // Time interval between spawns
    public float yOffset = 5f;           // Offset to prevent spawning too close to the screen edges
    public Vector2 playAreaMin;          // Minimum x and y bounds for spawning
    public Vector2 playAreaMax;          // Maximum x and y bounds for spawning

    private PlayerMovement player;
    private Transform playerTransform;
    private float timer;

    void Start()
    {
        player = FindObjectOfType<PlayerMovement>();
        playerTransform = player.transform;
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

        // Choose a random enemy prefab
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
                spawnPosition.x = Mathf.Clamp(playerPosition.x - spawnDistance, playAreaMin.x, playAreaMax.x);
                spawnPosition.y = Random.Range(playAreaMin.y + yOffset, playAreaMax.y - yOffset);
                break;
            case 1: // Spawn to the right
                spawnPosition.x = Mathf.Clamp(playerPosition.x + spawnDistance, playAreaMin.x, playAreaMax.x);
                spawnPosition.y = Random.Range(playAreaMin.y + yOffset, playAreaMax.y - yOffset);
                break;
            case 2: // Spawn above
                spawnPosition.y = Mathf.Clamp(playerPosition.y + spawnDistance, playAreaMin.y, playAreaMax.y);
                spawnPosition.x = Random.Range(playAreaMin.x + yOffset, playAreaMax.x - yOffset);
                break;
            case 3: // Spawn below
                spawnPosition.y = Mathf.Clamp(playerPosition.y - spawnDistance, playAreaMin.y, playAreaMax.y);
                spawnPosition.x = Random.Range(playAreaMin.x + yOffset, playAreaMax.x - yOffset);
                break;
        }

        return spawnPosition;
    }
}