using UnityEngine;
using System.Collections;

public class RandomTimedTarget : MonoBehaviour
{
    [Header("Timing (seconds)")]
    [Min(0f)] public float minVisibleTime = 1.0f;
    [Min(0f)] public float maxVisibleTime = 3.0f;
    [Min(0f)] public float minHiddenTime = 1.0f;
    [Min(0f)] public float maxHiddenTime = 4.0f;

    [Header("Target Components")]
    public Renderer[] renderers;
    public Collider[] colliders;

    private Coroutine cycleRoutine;

    private void Awake()
    {
        if (renderers == null || renderers.Length == 0)
            renderers = GetComponentsInChildren<Renderer>(true);

        if (colliders == null || colliders.Length == 0)
            colliders = GetComponentsInChildren<Collider>(true);
    }

    private void OnEnable()
    {
        cycleRoutine = StartCoroutine(VisibilityCycle());
    }

    // Called by projectile
    public void OnHit()
    {
        Debug.Log($"{name} was hit");

        // Hide immediately
        SetVisible(false);

        // Notify GameManager for scoring
        GameManager.Instance?.AddScore(1);

        // Start respawn timer
        StartCoroutine(RespawnAfterDelay());
    }

    private IEnumerator RespawnAfterDelay()
    {
        // Wait a random hidden time before reappearing
        float delay = Random.Range(minHiddenTime, maxHiddenTime);
        yield return new WaitForSeconds(delay);

        // Resume visibility cycle
        SetVisible(true);
    }

    private void OnDisable()
    {
        if (cycleRoutine != null)
            StopCoroutine(cycleRoutine);
    }

    private IEnumerator VisibilityCycle()
    {
        while (true)
        {
            SetVisible(true);
            yield return new WaitForSeconds(Random.Range(minVisibleTime, maxVisibleTime));

            SetVisible(false);
            yield return new WaitForSeconds(Random.Range(minHiddenTime, maxHiddenTime));
        }
    }

    private void SetVisible(bool visible)
    {
        foreach (var r in renderers)
            r.enabled = visible;

        foreach (var c in colliders)
            c.enabled = visible;
    }
}
