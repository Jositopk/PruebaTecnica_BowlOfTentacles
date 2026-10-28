using UnityEngine;

public interface IInteractable
{
    bool IsInteractable { get; }
    //metodo para definir la interaccion con el objeto
    void Interact(PlayerInteractor interactor);
    //metodo para mostrar el Mensaje de interaccion
    string ShowInteractionPrompt();
}
