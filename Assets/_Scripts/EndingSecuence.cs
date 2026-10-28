using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class EndingSequence : MonoBehaviour
{
    [Header("Tiempos")]
    [SerializeField] private float delayBeforeMessage = 1.5f;
    [SerializeField] private float messageDuration = 2f;
    [SerializeField] private float doorCloseDuration = 1f;
    [SerializeField] private float fadeDuration = 1.5f;

    [Header("Referencias")]
    [SerializeField] private CanvasGroup blackFade;
    [SerializeField] private GameObject endingPanel;

    [Header("Eventos")]
    [SerializeField] private UnityEvent onDoorClose;

    private GameManager gameManager;
    private bool hasStarted;

    private void Start()
    {
        gameManager = GameManager.Instance;

        if (endingPanel != null)
            endingPanel.SetActive(false);

        if (blackFade != null)
        {
            blackFade.alpha = 0f;
            blackFade.blocksRaycasts = false;
        }

        gameManager.OnStateChanged += HandleStateChanged;

        if (gameManager.CurrentState == GameState.ENDING)
            HandleStateChanged(GameState.ENDING);
    }

    private void OnDestroy()
    {
        if (gameManager != null)
            gameManager.OnStateChanged -= HandleStateChanged;
    }

    private void HandleStateChanged(GameState state)
    {
        if (state != GameState.ENDING || hasStarted)
            return;

        hasStarted = true;
        StartCoroutine(PlayEnding());
    }

    private IEnumerator PlayEnding()
    {
        // Esperar después de abrir la puerta.
        yield return new WaitForSecondsRealtime(delayBeforeMessage);

        // Mostrar el mensaje narrativo.
        UIManager.Instance.ShowInformation("No te vayas todavía", messageDuration);

        yield return new WaitForSecondsRealtime(messageDuration);

        // Cerrar la puerta mediante un evento configurable.
        onDoorClose?.Invoke();

        // Esperar a que termine de cerrarse.
        yield return new WaitForSecondsRealtime(doorCloseDuration);

        // Fundido a negro.
        if (blackFade != null)
        {
            float elapsed = 0f;

            while (elapsed < fadeDuration)
            {
                elapsed += Time.unscaledDeltaTime;

                blackFade.alpha = Mathf.Lerp(0f, 1f, Mathf.Clamp01(elapsed / fadeDuration));
                yield return null;
            }

            blackFade.alpha = 1f;
        }

        // Mostrar pantalla final.
        GameManager.Instance.SetState(GameState.FINISHED);
        UIManager.Instance.ShowEndingPanel();
    }

}