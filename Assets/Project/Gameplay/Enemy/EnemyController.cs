using Gameplay;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float speed {private get; set;}
    public int health {private get; set;}
    private RunState runState;
    private Transform player;

    void Awake()
    {
        player = FindAnyObjectByType<PlayerMovement>().transform;
        runState = FindAnyObjectByType<RunState>();
    }

    void Update()
    {
        if (player && runState.gameState != GameState.Paused)
        {
            transform.position = Vector3.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
        }
    }

    private void Deactivate()
    {
        GetComponent<ObjectPooler>().Deactivate();
    }

    public void DealDamage()
    {
        health--;
        if (health <= 0) Deactivate();
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ammo"))
        {
            DealDamage();
            collision.gameObject.GetComponent<ObjectPooler>().Deactivate();
        }
    }
}
