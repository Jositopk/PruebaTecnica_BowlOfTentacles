
using System.Collections;
using UnityEngine;

public class RotationAnimator : MonoBehaviour
{
    [Header("Configuración")]
    [SerializeField] private Vector3 rotationAngles = new Vector3(0f, 0f, 0f);
    [SerializeField, Min(0.01f)] private float rotationDuration = 0.8f;

    private Quaternion initialRotation;
    private Coroutine rotationCoroutine;
    private float progress = 0f;

    private void Awake()
    {
        initialRotation = transform.localRotation;
    }

    public void RotateForward()
    {
        StartRotation(1f);
    }

    public void RotateBackward()
    {
        StartRotation(0f);
    }

    public void ToggleRotation()
    {
        StartRotation(progress >= 0.5f ? 0f : 1f);
    }

    private void StartRotation(float targetProgress)
    {
        if (rotationCoroutine != null)
            StopCoroutine(rotationCoroutine);

        rotationCoroutine = StartCoroutine(
            AnimateRotation(targetProgress)
        );
    }

    private IEnumerator AnimateRotation(float targetProgress)
    {
        float startProgress = progress;
        float duration = rotationDuration *
                         Mathf.Abs(targetProgress - startProgress);

        if (duration <= 0f)
            yield break;

        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;

            float t = Mathf.Clamp01(elapsed / duration);
            t = Mathf.SmoothStep(0f, 1f, t);

            progress = Mathf.Lerp(startProgress, targetProgress, t);

            ApplyRotation();

            yield return null;
        }

        progress = targetProgress;
        ApplyRotation();

        rotationCoroutine = null;
    }

    private void ApplyRotation()
    {
        transform.localRotation = initialRotation *
            Quaternion.Euler(rotationAngles * progress);
    }
}
