using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] enemyprefab;
    public float spawnInterval = 1f; // time between spawns
    public float spawnRadius = 1.5f; // radius around the spawner to spawn enemies
    private float _spawnTimer; // timer to track time since last spawn
    
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        _spawnTimer += Time.deltaTime;

        if (_spawnTimer >= spawnInterval) // if the timer exceeds the spawn interval, spawn an enemy
        {
            _spawnTimer = 0f;
            SpawnEnemy();
        }
        
    }
    
    private void SpawnEnemy()
    {
        Vector2 spawnPos = (Vector2)transform.position + Random.insideUnitCircle * spawnRadius; // calculate a random position within the spawn radius
        int randomIndex = Random.Range(0, enemyprefab.Length);
        Instantiate(enemyprefab[randomIndex], spawnPos, Quaternion.identity); //chooses an enemy from a list?
    }
}
