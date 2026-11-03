using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using VContainer;

public class EnemySpawner : MonoBehaviour
{
    [Inject]private EnemyFactory enemyFactory;
    [Inject]private RunState runState;
    [SerializeField]private List<Transform> spawnPoints = new List<Transform>();
    [SerializeField]private List<WaveDefinition> waves = new List<WaveDefinition>();
    [SerializeField]private List<EnemyDefinition> enemyTypes = new List<EnemyDefinition>();
    [SerializeField]private float timeBetweenWaves;
    [SerializeField]private int startingWave;

    public Action<int> newWave;
    private bool spawningPaused;

    private int curWave;
    private PlayableDirector playableDirector;

    void Awake()
    {
        playableDirector = GetComponent<PlayableDirector>();
    }

    private void Start()
    {
        runState.stateChanged += StateChanged;

        curWave = startingWave - 1;
        StartNextWave();
    }

    private void StateChanged(GameState gameState)
    {
        if (gameState == GameState.Paused || gameState == GameState.GameOver)
        {
            playableDirector.Pause();
            spawningPaused = true;
        }
        else if (gameState == GameState.Run)
        {
            if (spawningPaused)
            {
                playableDirector.Play();
                spawningPaused = false;
            }
        }
    }

    private void SpawnEnemies(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            GameObject enemy = enemyFactory.CreateEnemy(enemyTypes[UnityEngine.Random.Range(0, enemyTypes.Count)]);
            enemy.transform.position = spawnPoints[UnityEngine.Random.Range(0, spawnPoints.Count)].position;
        }
    }

    public void SpawnEnemy(int enemyIndex)
    {
        GameObject enemy = enemyFactory.CreateEnemy(enemyTypes[enemyIndex]);
        enemy.transform.position = spawnPoints[UnityEngine.Random.Range(0, spawnPoints.Count)].position;
        
        enemy.GetComponent<EnemyController>().health = enemyTypes[enemyIndex].health;
    }

    public void EndWave()
    {
        Invoke(nameof(StartNextWave), timeBetweenWaves);
    }

    private void StartNextWave()
    {
        curWave++;
        if (curWave >= waves.Count)
        {
            Debug.Log("Game Won");
            runState.ChangeGameState(GameState.GameOver);
            return;
        }

        playableDirector.playableAsset = waves[curWave].waveTimeline;
        playableDirector.Play();

        newWave?.Invoke(curWave);
    }
}
