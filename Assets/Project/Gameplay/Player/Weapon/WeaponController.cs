using UnityEngine;
using UnityEngine.InputSystem;

public class WeaponController : MonoBehaviour
{
    [SerializeField]private float fireRate;
    [SerializeField]private float shootForce;
    [SerializeField]private GameObject ammoPrefab;

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
        GameObject spawnedAmmo = Instantiate(ammoPrefab, shootPoint.position, Quaternion.identity);
        spawnedAmmo.GetComponent<Rigidbody>().AddForce(transform.forward * shootForce, ForceMode.Impulse);
    }
}
