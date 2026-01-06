using UnityEngine;
using UnityEngine.InputSystem;

public class XRProjectileShooter : MonoBehaviour
{
    [Header("Input")]
    [SerializeField] private InputActionProperty shootAction;

    [Header("Projectile")]
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private float shootForce = 15f;

    void OnEnable()
    {
        shootAction.action.Enable();
        shootAction.action.performed += OnShoot;
    }

    void OnDisable()
    {
        shootAction.action.performed -= OnShoot;
        shootAction.action.Disable();
    }

    private void OnShoot(InputAction.CallbackContext ctx)
    {
        Fire();
    }

    private void Fire()
    {
        Debug.Log($"Prefab: {projectilePrefab}, SpawnPoint: {spawnPoint}");

        if (!projectilePrefab || !spawnPoint)
        {
            Debug.LogWarning("XRProjectileShooter: Missing prefab or spawn point");
            return;
        }

        GameObject projectile = Instantiate(
            projectilePrefab,
            spawnPoint.position,
            spawnPoint.rotation
        );

        Rigidbody rb = projectile.GetComponent<Rigidbody>();
        if (!rb)
        {
            Debug.LogError("Projectile prefab must have a Rigidbody");
            Destroy(projectile);
            return;
        }

        rb.AddForce(spawnPoint.forward * shootForce, ForceMode.Impulse);

        Debug.DrawRay(spawnPoint.position, spawnPoint.forward * 0.5f, Color.red, 1f);
    }
}
