using UnityEngine;

public class MovingTarget : RandomTimedTarget
{
    [Header("Movement Settings")]
    public float movementRange = 1.0f; // total left/right distance
    public float speed = 0.5f;         // units per second

    private Vector3 startPosition;
    private int direction = 1;

    private void Start()
    {
        startPosition = transform.position;
    }

    private void Update()
    {
        // Only move when visible
        if (colliders.Length > 0 && colliders[0].enabled)
        {
            Vector3 pos = transform.position;
            pos.x += direction * speed * Time.deltaTime;

            // Check bounds
            if (pos.x > startPosition.x + movementRange / 2)
            {
                pos.x = startPosition.x + movementRange / 2;
                direction = -1;
            }
            else if (pos.x < startPosition.x - movementRange / 2)
            {
                pos.x = startPosition.x - movementRange / 2;
                direction = 1;
            }

            transform.position = pos;
        }
    }
}
