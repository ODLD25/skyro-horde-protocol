using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public interface ICommand {
    public void Run();
}

public class MoveCommand : ICommand
{
    public Rigidbody rb;
    public Vector3 force;

    public MoveCommand(Rigidbody rb, Vector3 force)
    {
        this.rb = rb;
        this.force = force;
    }

    public void Run()
    {
        rb.AddForce(force, ForceMode.Force);
    }
}

public class FireCommand : ICommand
{
    public WeaponController weaponController;

    public FireCommand(WeaponController weaponController)
    {
        this.weaponController = weaponController;
    }

    public void Run()
    {
        weaponController.Shoot();
    }
}

public class PauseCommand : ICommand
{
    public RunState runState;

    public PauseCommand(RunState runState)
    {
        this.runState = runState;
    }

    public void Run()
    {
        if (runState.gameState == GameState.Paused)
        {
            runState.ChangeGameState(GameState.Run);
        }
        else
        {
            runState.ChangeGameState(GameState.Paused);
        }
    }
}

public class RetryCommand : ICommand
{
    public void Run()
    {
        SceneManager.LoadScene("Arena");
    }
}

public class RotateCommand : ICommand
{
    public Transform targetTransform;
    public Transform cam;

    public RotateCommand(Transform targetTransform, Transform cam)
    {
        this.targetTransform = targetTransform;
        this.cam = cam;
    }

    public void Run()
    {
        Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());

        RaycastHit hit;
        
        if (Physics.Raycast(ray, out hit, 100))
        {
            hit.point = new Vector3(hit.point.x, targetTransform.position.y, hit.point.z);
            targetTransform.LookAt(Camera.main.ScreenToWorldPoint(new Vector3(Mouse.current.position.ReadValue().x, Mouse.current.position.ReadValue().y, Vector3.Distance(cam.position, hit.point))));
            targetTransform.rotation = Quaternion.Euler(0, targetTransform.eulerAngles.y, targetTransform.eulerAngles.z);
        }
    }
}