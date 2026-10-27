using UnityEngine;
using UnityEngine.InputSystem;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    [Header("Interfaces")]
    [SerializeField] private NoteUI noteUI;
    [SerializeField] private TMPro.TMP_Text interactionText;
    [SerializeField] private FPSController playerController;
private bool noteIsOpen = false;

    

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
        if (noteIsOpen &&
            Keyboard.current != null &&
            Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            CloseNote();
        }
    }
    //////////////////////////////////////
    //metodos para las notas
    #region NoteUI
    
    public void ShowNote(NoteData note)
    {
        if (noteIsOpen){return;}
        noteIsOpen = true;
        OpenUI();
        noteUI.ShowNote(note);
    }

    public void CloseNote()
    {
        if (!noteIsOpen){return;}
        noteUI.CloseNote();
        CloseUI();
        noteIsOpen = false;
    }
    #endregion
    //////////////////////////////////////
    //Metodos para el mensaje de interaccion
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
    //////////////////////////////////////
    //metodos para abrir y cerrar la UI
    #region OpenCloseUI
    public void OpenUI()
    {
        playerController.enabled = false;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void CloseUI()
    {
        playerController.enabled = true;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    #endregion
}
