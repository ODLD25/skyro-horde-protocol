using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;
using UnityEngine.Serialization;

public class EnemyTable
{
    [FormerlySerializedAs("allCharecters")]
    [TableList(IsReadOnly = true, AlwaysExpanded = true), ShowInInspector]
    private readonly List<EnemyWrapper> allEnemies;

    public EnemyDefinition this[int index]
    {
        get { return this.allEnemies[index].Enemy; }
    }

    public EnemyTable(IEnumerable<EnemyDefinition> enemies)
    {
        this.allEnemies = enemies.Select(x => new EnemyWrapper(x)).ToList();
    }

    private class EnemyWrapper
    {
        private EnemyDefinition enemy; // Character is a ScriptableObject and would render a unity object
                                        // field if drawn in the inspector, which is not what we want.

        public EnemyDefinition Enemy
        {
            get { return this.enemy; }
        }

        public EnemyWrapper(EnemyDefinition enemy)
        {
            this.enemy = enemy;
        }

        [TableColumnWidth(120)]
        [ShowInInspector]
        public string Name { get { return this.enemy.name; } set { this.enemy.name = value; EditorUtility.SetDirty(this.enemy); } }

        [ShowInInspector, ProgressBar(0, 10)]
        public int Health { get { return this.enemy.health; } set { this.enemy.health = value; EditorUtility.SetDirty(this.enemy); } }

        [ShowInInspector, ProgressBar(0, 10)]
        public float Speed { get { return this.enemy.speed; } set { this.enemy.speed = value; EditorUtility.SetDirty(this.enemy); } }
    }
}
