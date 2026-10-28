using UnityEngine;

public class TestInteractable : AInteractable
{
    public override void Interact(PlayerInteractor interactor)
    {
        Debug.Log("Interaccion con TestInteractable");
    }
}
