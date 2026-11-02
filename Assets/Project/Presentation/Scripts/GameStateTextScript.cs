using UnityEngine;
using TMPro;

public class GameStateTextScript : MonoBehaviour
{
    [SerializeField]private RunState runState;
    [SerializeField]private TextMeshProUGUI stateText;

    private void Awake() {
        runState.stateChanged += OnStateChanged;
    }

    private void OnStateChanged(GameState newGameState)
    {
        stateText.text = newGameState.ToString();
    }
}
