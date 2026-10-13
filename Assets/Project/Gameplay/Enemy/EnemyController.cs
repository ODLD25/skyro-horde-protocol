using Gameplay;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField]private float speed;
    private Transform player;

    void Awake()
    {
        player = FindAnyObjectByType<PlayerMovement>().transform;
    }

    void Update()
    {
        if (player)
        {
            transform.position = Vector3.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ammo"))
        {
            Destroy(gameObject);
        }
    }
}
