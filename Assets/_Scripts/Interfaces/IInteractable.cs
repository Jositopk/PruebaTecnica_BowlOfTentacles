using UnityEngine;

public interface IInteractable
{
    bool IsInteractable { get; }
    //metodo para definir la interaccion con el objeto
    void Interact();
    //metodo para mostrar el Mensaje de interaccion
    string ShowInteractionPrompt();
}
