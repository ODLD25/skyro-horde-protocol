#if UNITY_EDITOR
using Sirenix.OdinInspector;
using Sirenix.Utilities;
using System.Linq;
using UnityEditor;
#endif

[GlobalConfig("Assets/Project/Data/Enemies")]
    public class EnemyOverview : GlobalConfig<EnemyOverview> 
    {
        [ReadOnly]
        [ListDrawerSettings(ShowFoldout = true)]
        public EnemyDefinition[] AllEnemies;

#if UNITY_EDITOR
        [Button(ButtonSizes.Medium), PropertyOrder(-1)]
        public void UpdateEnemyOverview()
        {
            // Finds and assigns all scriptable objects of type EnemyDefinition
            this.AllEnemies = AssetDatabase.FindAssets("t:EnemyDefinition")
                .Select(guid => AssetDatabase.LoadAssetAtPath<EnemyDefinition>(AssetDatabase.GUIDToAssetPath(guid)))
                .ToArray();
        }
#endif
    }