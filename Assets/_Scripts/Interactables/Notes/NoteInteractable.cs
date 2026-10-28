using UnityEngine;
using UnityEngine.Events;

public class NoteInteractable : AInteractable
{  
    [SerializeField] private NoteData noteData;
    [SerializeField] private UnityEvent onClosed = new UnityEvent();
    public override string ShowInteractionPrompt()
    {
        return "Pulsa E para leer la nota";
    }
    public override void Interact(PlayerInteractor interactor)
    {
        UIManager.Instance.ShowNote(noteData,onClosed.Invoke);
            
    }
}
