using Gameplay;
using UnityEngine;

public class RunState : MonoBehaviour
{
    public GameState gameState {get; private set;}

    public static RunState instance;

    void Awake()
    {
        gameState = GameState.Run;
    
        instance = this;
    }

    public void ChangeGameState(GameState newGameState)
    {
        gameState = newGameState;
    }

    public void Submit(ICommand command)
    {
        switch (command)
        {
            case MoveCommand moveCommand:
                if (gameState == GameState.Run)
                {
                    moveCommand.Run();
                }
            break;

            case FireCommand fireCommand:
                if (gameState == GameState.Run)
                {
                    fireCommand.Run();
                }
            break;
            
            case PauseCommand pauseCommand:
                if (gameState != GameState.GameOver)
                {
                    pauseCommand.Run();
                }
            break;
            
            case RetryCommand retryCommand:
                if (gameState == GameState.GameOver)
                {
                    retryCommand.Run();
                }
            break;
            case RotateCommand rotateCommand:
                if (gameState == GameState.Run)
                {
                    rotateCommand.Run();
                }
            break;
        }
    }
}

public enum GameState
{
    Run,
    Paused,
    GameOver
}