using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class StartingSequence : MonoBehaviour
{
     [Header("Tiempos")]
    [SerializeField] private float blackScreenDuration = 2f;
    [SerializeField] private float fadeDuration = 1.5f;

    [Header("Referencias")]
    [SerializeField] private CanvasGroup blackFade;

    [Header("Eventos")]
    [SerializeField] private UnityEvent onFadeCompleted;

    private void Awake()
    {
        if (blackFade == null) return;

        blackFade.alpha = 1f;
        blackFade.blocksRaycasts = true;
        blackFade.interactable = false;
    }

    private IEnumerator Start()
    {
        if (blackFade == null)
        {
            Debug.LogError("IntroSequence: falta Black Fade.");
            yield break;
        }

        // Esperar con la pantalla completamente negra.
        yield return new WaitForSecondsRealtime(blackScreenDuration);
        onFadeCompleted?.Invoke();
        // Revelar progresivamente el escenario.
        float elapsed = 0f;

        while (elapsed < fadeDuration)
        {
            elapsed += Time.unscaledDeltaTime;

            float t = Mathf.Clamp01(elapsed / fadeDuration);
            t = Mathf.SmoothStep(0f, 1f, t);

            blackFade.alpha = 1f - t;

            yield return null;
        }

        blackFade.alpha = 0f;
        blackFade.blocksRaycasts = false;

        // Notificar que hemos terminado.
        
    }
}
