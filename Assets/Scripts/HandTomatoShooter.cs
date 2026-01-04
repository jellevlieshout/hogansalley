using UnityEngine;
using UnityEngine.InputSystem;

public class HandTomatoShooter : MonoBehaviour
{
    [Header("References")]
    public InputActionProperty shootAction;
    public Transform spawnPoint;
    public GameObject tomatoPrefab;
    public float shootForce = 15f;

    void OnEnable()
    {
        shootAction.action.performed += OnShoot;
    }

    void OnDisable()
    {
        shootAction.action.performed -= OnShoot;
    }

    void OnShoot(InputAction.CallbackContext ctx)
    {
        ShootTomato();
    }

    void ShootTomato()
    {
        if (tomatoPrefab == null || spawnPoint == null) return;

        GameObject tomato = Instantiate(tomatoPrefab, spawnPoint.position, spawnPoint.rotation);
        Rigidbody rb = tomato.GetComponent<Rigidbody>();
        if (rb != null)
            rb.AddForce(spawnPoint.forward * shootForce, ForceMode.Impulse);
    }
}
