using UnityEngine;
using UnityEngine.InputSystem;
using VContainer;

public class WeaponController : MonoBehaviour
{
    [SerializeField]private float fireRate;
    [SerializeField]private float shootForce;
    [Inject]private AmmoPool ammoPool;

    [Header("References")]
    [SerializeField]private Transform shootPoint;
    private Horde inputActions;

    private bool isShooting;
    private bool canShoot;

    /*void Start()
    {
        canShoot = true;
    }

    /*void Update()
    {
        if (inputActions.Player.Fire.ReadValue<float>() != 0) isShooting = true;
        else isShooting = false;

        if (isShooting && canShoot)
        {
            Shoot();
            canShoot = false;

            Invoke(nameof(ResetCanShoot), fireRate);
        }
    }*/

    public void Shoot()
    {
        GameObject spawnedAmmo = ammoPool.GetObject();
        spawnedAmmo.transform.position = shootPoint.position;
        spawnedAmmo.GetComponent<Rigidbody>().AddForce(transform.forward * shootForce, ForceMode.Impulse);
    }
}
