using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]private ObjectPool enemyPool;
    [SerializeField]private List<Transform> spawnPoints = new List<Transform>();
    [SerializeField]private int amountToSpawn;

    private void Start() {
        SpawnEnemies(amountToSpawn);
    }

    private void SpawnEnemies(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            GameObject enemy = enemyPool.GetObject();
            enemy.transform.position = spawnPoints[Random.Range(0, spawnPoints.Count)].position;
        }
    }
}
