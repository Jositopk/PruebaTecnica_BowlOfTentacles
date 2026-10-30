using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    [Header("Interfaces")]
    [SerializeField] private NoteUI noteUI;
    [SerializeField] private BoxUI boxUI;
    [SerializeField] private TMPro.TMP_Text interactionText;
    [SerializeField] private TMPro.TMP_Text informationText;
    [SerializeField] private GameObject endingPanel;

    private bool noteIsOpen = false;
    private bool boxIsOpen = false;
    public bool IsUIOpen => noteIsOpen || boxIsOpen;

    [Header("Mensajes informativos")]
    [SerializeField] private float charactersPerSecond = 25f;
    [SerializeField] private float minDisplayTime = 1.5f;
    [SerializeField] private float maxDisplayTime = 6f;
    [SerializeField] private float fadeDuration = 0.5f;

    private Coroutine informationCoroutine;
    private bool isBlockingMessage = false;
    private Action currentNoteClosedCallback;

    //Patron Singelton para que solo exista una instancia de UIManager en la escena.
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    private void Update()
    {
        if (Keyboard.current == null ||
            !Keyboard.current.escapeKey.wasPressedThisFrame)
            return;

        if (noteIsOpen)
            CloseNote();
        else if (boxIsOpen)
            CloseBox();
    }
    //////////////////////////////////////
    //metodos para las notas
    //////////////////////////////////////
    #region NoteUI

    public void ShowNote(NoteData note, Action onClosed = null)
    {
        //Guardamos el evento si lo tiene
        currentNoteClosedCallback = onClosed;

        if (noteIsOpen || boxIsOpen) { return; }
        noteIsOpen = true;
        OpenUI();
        noteUI.ShowNote(note);
    }

    public void CloseNote()
    {
        if (!noteIsOpen) { return; }

        //Ejecutar evento al cerrar
        Action callback = currentNoteClosedCallback;
        currentNoteClosedCallback = null;
        callback?.Invoke();

        noteUI.CloseNote();
        CloseUI();
        noteIsOpen = false;
    }
    #endregion
    //////////////////////////////////////
    /// // Metodos para abrir y cerrar la UI de la caja
    /// //////////////////////////////////////
    #region BoxUI
    public void ShowBox(BoxPuzzle boxPuzzle)
    {
        if (boxIsOpen || noteIsOpen) { return; }
        boxIsOpen = true;
        OpenUI();
        boxUI.ShowBox(boxPuzzle);
    }
    public void CloseBox()
    {
        if (!boxIsOpen) { return; }
        boxUI.CloseBox();
        CloseUI();
        boxIsOpen = false;
    }
    #endregion
    //////////////////////////////////////
    //Metodos para el mensaje de interaccion
    //////////////////////////////////////
    #region InteractionPrompt
    public void ShowInteractionPrompt(string prompt)
    {
        if (interactionText != null)
        {
            interactionText.gameObject.SetActive(true);
            interactionText.text = prompt;
        }
    }
    public void HideInteractionPrompt()
    {
        if (interactionText != null)
        {
            interactionText.gameObject.SetActive(false);
            interactionText.text = "";
        }
    }
    #endregion

    #region InformationText
    // Mensaje normal: duración automática, sin pausa.
    public void ShowInformation(string message)
    {
        if (informationText == null ||
            string.IsNullOrWhiteSpace(message) ||
            isBlockingMessage)
            return;

        if (informationCoroutine != null)
            StopCoroutine(informationCoroutine);

        informationCoroutine = StartCoroutine(
            ShowInformationCoroutine(message)
        );
    }

    // Mensaje con duración fija y pausa.
    public void ShowInformation(
    string message,
    float displayTime,
    Action onCompleted = null)
    {
        if (informationCoroutine != null)
            return;

        informationCoroutine = StartCoroutine(
            ShowInformationCoroutine(message, displayTime, onCompleted)
        );
    }

    private IEnumerator ShowInformationCoroutine(string message)
    {
        informationText.text = message;
        informationText.alpha = 1f;
        informationText.gameObject.SetActive(true);

        // Calcular duracion segun longitud del mensaje.
        float displayTime = Mathf.Clamp(message.Length / charactersPerSecond, minDisplayTime, maxDisplayTime);

        yield return new WaitForSecondsRealtime(displayTime);

        // Fade-out
        float elapsed = 0f;
        while (elapsed < fadeDuration)
        {
            elapsed += Time.unscaledDeltaTime;
            informationText.alpha = 1f - Mathf.Clamp01(elapsed / fadeDuration);
            yield return null;
        }
        informationText.gameObject.SetActive(false);
        informationCoroutine = null;
    }


    private IEnumerator ShowInformationCoroutine(
        string message,
        float displayTime,
        Action onCompleted)
    {
        isBlockingMessage = true;

        informationText.text = message;
        informationText.alpha = 1f;
        informationText.gameObject.SetActive(true);

        // Mantener mensaje visible.
        yield return new WaitForSecondsRealtime(displayTime);

        // Fade-out.
        float elapsed = 0f;

        while (elapsed < fadeDuration)
        {
            elapsed += Time.unscaledDeltaTime;

            informationText.alpha =
                1f - Mathf.Clamp01(elapsed / fadeDuration);

            yield return null;
        }

        informationText.alpha = 0f;
        informationText.gameObject.SetActive(false);

        informationCoroutine = null;
        isBlockingMessage = false;

        // Notificar que el mensaje ha terminado.
        onCompleted?.Invoke();
    }


    #endregion
    public void ShowEndingPanel()
    {
        endingPanel.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
    //////////////////////////////////////
    //metodos para abrir y cerrar la UI
    //////////////////////////////////////
    #region OpenCloseUI
    public void OpenUI()
    {
        GameManager.Instance.SetPlayerControl(false);

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void CloseUI()
    {
        GameManager.Instance.SetPlayerControl(true);

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    #endregion

}
