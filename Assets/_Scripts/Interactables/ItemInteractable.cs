
using UnityEngine;

public class ItemInteractable : AInteractable
{
    [Header("Objeto")]
    [SerializeField] private ItemData itemData;

    public override void Interact(PlayerInteractor interactor)
    {
        if (interactor.Inventory.TryPickUp(itemData,gameObject))
        {
            UIManager.Instance.ShowInformation(
                $"Has recogido: {itemData.itemName}"
            );
        }
        else
        {
            UIManager.Instance.ShowInformation(
                "No puedo llevar más objetos."
            );
        }
    }

    public override string ShowInteractionPrompt()
    {
        return $"Pulsa E para recoger {itemData.itemName}";
    }
}
