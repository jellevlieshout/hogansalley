using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Collider))]
public class TomatoProjectile : MonoBehaviour
{
    [SerializeField] private float lifeTime = 5f;

    private void Start()
    {
        Destroy(gameObject, lifeTime); // auto-cleanup
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log($"Projectile hit: {collision.collider.name}");
        RandomTimedTarget target = collision.collider.GetComponent<RandomTimedTarget>();
        if (target != null)
            target.OnHit();

        Destroy(gameObject);
    }
}
