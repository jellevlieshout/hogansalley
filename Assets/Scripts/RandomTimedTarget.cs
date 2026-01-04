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
        // Auto-fill if not set manually
        if (renderers == null || renderers.Length == 0)
            renderers = GetComponentsInChildren<Renderer>(true);

        if (colliders == null || colliders.Length == 0)
            colliders = GetComponentsInChildren<Collider>(true);
    }

    private void OnEnable()
    {
        cycleRoutine = StartCoroutine(VisibilityCycle());
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
