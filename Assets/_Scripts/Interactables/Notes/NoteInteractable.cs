using UnityEngine;

public class NoteInteractable : AInteractable
{  
    [SerializeField] private NoteData noteData;

    public override string ShowInteractionPrompt()
    {
        return "Pulsa E para leer la nota";
    }
    public override void Interact()
    {
        UIManager.Instance.ShowNote(noteData);
            
    }
}
