using UnityEngine;

public abstract class AInteractable : MonoBehaviour, IInteractable
{
    [Header("Interaccion")]
    [SerializeField]
    private bool isInteractable = true;
    public bool IsInteractable => isInteractable;

    // Cada objeto hijo DEBE implementar su propia interaccion.
    public abstract void Interact();

    // Comportamiento comun que los hijos pueden sobrescribir.
    // De momento lo comprobamos mediante la consola.
    public virtual string ShowInteractionPrompt()
    {
        return "Pulsa E para interactuar";
    }
}