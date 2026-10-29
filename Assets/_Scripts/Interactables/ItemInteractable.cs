
using System;
using UnityEngine;

public class ItemInteractable : AInteractable
{
    [Header("Objeto")]
    [SerializeField] private ItemData itemData;

    [SerializeField]private string mensaje = "";
    public override void Interact(PlayerInteractor interactor)
    {
        if (interactor.Inventory.TryPickUp(itemData,gameObject))
        {
            UIManager.Instance.ShowInformation(mensaje);
        }
        else
        {
            UIManager.Instance.ShowInformation("No puedo llevar más objetos.");
        }
    }

    public override string ShowInteractionPrompt()
    {
        return $"Pulsa E para recoger {itemData.itemName}";
    }
}
