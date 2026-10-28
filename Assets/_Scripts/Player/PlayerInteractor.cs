
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInput))]
public class PlayerInteractor : MonoBehaviour
{
    [Header("Referencias")]
    [SerializeField] private Camera playerCamera;
    [Header("Interaccion")]
    [SerializeField] private float interactionDistance = 3f;

    // Capas con las que puede colisionar el raycast.
    // Por defecto incluye todas (tambien paredes).
    [SerializeField] private LayerMask raycastLayers = ~0;

    // Referencias al sistema de entrada.
    private PlayerInput playerInput;
    private InputAction interactAction;

    // Objeto al que estamos apuntando actualmente.
    private IInteractable currentInteractable;

    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();

        // Si no se ha asignado una camara, la buscamos
        // entre los hijos del jugador.
        if (playerCamera == null)
            playerCamera = GetComponentInChildren<Camera>();

        // Obtenemos la accion Interact del Input System.
        interactAction = playerInput.actions.FindAction(
            "Interact", true
        );
    }

    private void Update()
    {
        DetectInteractable();
        HandleInteraction();
    }

    private void DetectInteractable()
    {
        IInteractable detectedInteractable = null;

        // Lanzamos un rayo desde la camara hacia delante.
        if (Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out RaycastHit hit, interactionDistance, raycastLayers, QueryTriggerInteraction.Ignore))
        {
            // Comprobamos si el objeto impactado o alguno
            // de sus padres implementa IInteractable.
            detectedInteractable =
                hit.collider.GetComponentInParent<IInteractable>();
        }

        // Solo actuamos cuando cambia el objeto apuntado.
        if (detectedInteractable != currentInteractable)
        {
            currentInteractable = detectedInteractable;

            if (currentInteractable != null)
            {
                string prompt = currentInteractable.ShowInteractionPrompt();

                UIManager.Instance.ShowInteractionPrompt(prompt);
            }
            else
            {
                UIManager.Instance.HideInteractionPrompt();
            }
        }
    }

    private void HandleInteraction()
    {
        // Solo interactuamos si existe un objeto valido
        // y se ha pulsado la accion Interact.
        if (currentInteractable != null &&
            interactAction.WasPressedThisFrame())
        {
            currentInteractable.Interact();
        }
    }
    private void OnDisable()
    {
        currentInteractable = null;

        if (UIManager.Instance != null)
            UIManager.Instance.HideInteractionPrompt();
    }
}
