using TMPro;
using UnityEngine;

public class WaveText : MonoBehaviour
{
    [SerializeField]private TextMeshProUGUI waveText;
    [SerializeField]private EnemySpawner enemySpawner;

    void Awake()
    {
        enemySpawner.newWave += OnNewWave;
    }

    private void OnNewWave(int waveNumber)
    {
        waveText.text = waveNumber.ToString();
    }
}
