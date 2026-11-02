using Gameplay;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField]private float speed;    
    private RunState runState;
    private Transform player;

    void Awake()
    {
        player = FindAnyObjectByType<PlayerMovement>().transform;
        runState = FindAnyObjectByType<RunState>();
    }

    void Update()
    {
        if (player && runState.gameState == GameState.Run)
        {
            transform.position = Vector3.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ammo"))
        {
            GetComponent<ObjectPooler>().Deactivate();
        }
    }
}
