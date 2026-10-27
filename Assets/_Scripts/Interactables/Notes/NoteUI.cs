using UnityEngine;

public class NoteUI : MonoBehaviour
{
    [SerializeField] private TMPro.TMP_Text titleText;
    [SerializeField] private TMPro.TMP_Text contentText;


    public void ShowNote(NoteData noteData)
    {
        //titleText.text = noteData.title;
        contentText.text = noteData.content;
        gameObject.SetActive(true);
    }
    public void CloseNote()
    {
        gameObject.SetActive(false);
    }
}
