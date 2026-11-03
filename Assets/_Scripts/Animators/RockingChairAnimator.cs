
using System.Collections;
using UnityEngine;

public class RockingChairAnimator : MonoBehaviour
{
    [Header("Referencias")]
    [SerializeField] private Transform rockingVisual;

    [Header("Balanceo")]
    [SerializeField] private Vector3 rotationAngles = new Vector3(8f, 0f, 0f);
    [SerializeField, Min(0.1f)] private float duration = 12.2f;
    [SerializeField, Min(1)] private int cycles = 7;
    [SerializeField, Min(1f)] private float finalSpeedMultiplier = 1.3f;

    private Quaternion initialRotation;
    private Coroutine rockingCoroutine;

    public void StartRocking()
    {
        if (rockingVisual == null)
            return;

        if (rockingCoroutine != null)
            StopRocking();

        // Guardar la rotacion actual despues del TransformPositioner.
        initialRotation = rockingVisual.localRotation;

        rockingCoroutine = StartCoroutine(Rock());
    }

    public void StopRocking()
    {
        if (rockingCoroutine == null)
            return;

        StopCoroutine(rockingCoroutine);
        rockingCoroutine = null;

        rockingVisual.localRotation = initialRotation;
    }

    private IEnumerator Rock()
    {
        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;

            float t = Mathf.Clamp01(elapsed / duration);

            // Aceleracion progresiva.
            float acceleration = finalSpeedMultiplier - 1f;

            float phase = (t + 0.5f * acceleration * t * t) /
                          (1f + 0.5f * acceleration);

            // Movimiento de ida y vuelta.
            float angle = Mathf.Sin(
                phase * cycles * 2f * Mathf.PI
            );

            rockingVisual.localRotation = initialRotation *
                Quaternion.Euler(rotationAngles * angle);

            yield return null;
        }

        rockingVisual.localRotation = initialRotation;
        rockingCoroutine = null;
    }
}
