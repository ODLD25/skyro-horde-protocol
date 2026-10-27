using Gameplay;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    [SerializeField]private RunState runState;
    [SerializeField]private Rigidbody playerRb;
    private Horde inputActions;

    void Awake()
    {
        inputActions = new Horde();
        inputActions.Player.Enable();
        inputActions.Player.Fire.performed += SendShootCommand;
        inputActions.Player.Pause.performed += SendPauseCommand;
        inputActions.Player.Retry.performed += SendRetryCommand;
    }

    void Update()
    {
        if (playerRb)
        {
            runState.Submit(new MoveCommand(playerRb, Vector3.forward * inputActions.Player.Move.ReadValue<Vector2>().y * 30 + Vector3.right * inputActions.Player.Move.ReadValue<Vector2>().x * 30));
            runState.Submit(new RotateCommand(playerRb.transform, Camera.main.transform));
        }
    }

    private void SendShootCommand(InputAction.CallbackContext callbackContext)
    {
        runState.Submit(new FireCommand(FindAnyObjectByType<WeaponController>()));
    }

    private void SendPauseCommand(InputAction.CallbackContext callbackContext)
    {
        runState.Submit(new PauseCommand(runState));
    }

    private void SendRetryCommand(InputAction.CallbackContext callbackContext)
    {
        runState.Submit(new RetryCommand());
    }

    void OnDisable()
    {
        inputActions.Player.Disable();
    }
}
