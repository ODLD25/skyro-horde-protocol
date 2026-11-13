using System;
using Gameplay;
using UnityEngine;

public class RunState : MonoBehaviour
{
    public GameState gameState {get; private set;}
    public Action<GameState> stateChanged;

    public static RunState instance;

    void Awake()
    {
        gameState = GameState.Run;
    
        instance = this;
    }

    public void ChangeGameState(GameState newGameState)
    {
        gameState = newGameState;
        stateChanged?.Invoke(newGameState);
    }

    public void Submit(ICommand command)
    {
        switch (command)
        {
            case MoveCommand moveCommand:
                if (gameState != GameState.Paused)
                {
                    moveCommand.Run();
                }
            break;

            case FireCommand fireCommand:
                if (gameState != GameState.Paused)
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
                if (gameState != GameState.Paused)
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