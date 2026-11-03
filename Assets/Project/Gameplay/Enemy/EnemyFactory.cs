using UnityEngine;
using VContainer;

public class EnemyFactory : MonoBehaviour
{
    [Inject]private EnemyPool enemyPool;

    public GameObject CreateEnemy(EnemyDefinition enemyDefinition)
    {
        GameObject enemy = enemyPool.GetObject();
        enemy.GetComponent<EnemyController>().health = enemyDefinition.health;
        enemy.GetComponent<EnemyController>().speed = enemyDefinition.speed;
    
        return enemy;
    }
}
