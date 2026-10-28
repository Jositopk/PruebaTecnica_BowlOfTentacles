using Unity.VisualScripting;
using UnityEngine;

public class DoorInteractable : AInteractable
{   
    bool unlocked = false;
    public override void Interact()
    {
        if (unlocked)
        {
            
        }
        else
        {
            UIManager.Instance.ShowInformation("La llave de repuesto está en su caja");
        }
    }

    public override string ShowInteractionPrompt()
    {
        return "Pulsa E para abrir la puerta";
    }
}
