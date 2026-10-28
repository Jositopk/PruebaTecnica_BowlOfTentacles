using UnityEngine;
using UnityEngine.UI;

public class BoxInteractable : AInteractable
{
    [SerializeField] private BoxPuzzle boxPuzzle;
    public override void Interact(PlayerInteractor interactor)
    {
        if (boxPuzzle.IsSolved){return;}
        UIManager.Instance.ShowBox(boxPuzzle);
    }
    public override string ShowInteractionPrompt()
    {
        if (boxPuzzle.IsSolved){return "";}
        return "Pulsa E para ver la caja";
    }
}
