using UnityEngine;

[CreateAssetMenu(fileName = "EnemyDefinition", menuName = "Scriptable Objects/EnemyDefinition")]
public class EnemyDefinition : ScriptableObject
{
    public int health = 1;
    public float speed = 3.5f;
}
