using UnityEngine;
using VContainer;

public class EnemySpawner : MonoBehaviour
{
    [Inject] private EnemyObjectPool objectPool;

    void Start()
    {
        SpawnEnemy();
    }

    private void SpawnEnemy()
    {
        GameObject enemy = objectPool.GetObject();
    }
}
