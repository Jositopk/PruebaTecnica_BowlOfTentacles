
using UnityEngine;
using UnityEngine.Events;

public class DoorInteractable : AInteractable
{
    [Header("Requisitos")]
    [SerializeField] private ItemData requiredKey;
     [SerializeField] private UnityEvent OnDoorOppened;
    [SerializeField] private UnityEvent OnDoorClosed;
    
    public override void Interact(PlayerInteractor interactor)
    {
        // Comprobar si tenemos la llave.
        if (!interactor.Inventory.HasItem(requiredKey))
        {
            UIManager.Instance.ShowInformation(
                "La llave de repuesto está en su caja"
            );
            return;
        }

        // Comprobar la progresión del juego. En caso de que quedasen mas puzzles por hacer
        if (!GameManager.Instance.CanExit())
        {
            UIManager.Instance.ShowInformation(
                "Todavía no puedo salir."
            );
            return;
        }

        // Bloquear controles y evitar nuevas interacciones.
        OpenDoor();
        //La escena final se gestiona sola al cambiar de estado
    }

    public override string ShowInteractionPrompt()
    {
        return "Pulsa E para abrir la puerta";
    }
    public void OpenDoor()
    {
        OnDoorOppened?.Invoke();
        gameObject.SetActive(false);
        GameManager.Instance.SetState(GameState.ENDING);
    }
    public void CloseDoor()
    {
        OnDoorClosed?.Invoke();
        gameObject.SetActive(true);
    }
}
