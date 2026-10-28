using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class BoxUI : MonoBehaviour
{
    //referencia a los 3 botones
    [SerializeField] private Button lamparaButton;
    [SerializeField] private Button radioButton;
    [SerializeField] private Button tazaButton;
    private BoxPuzzle currentPuzzle;
    public void Awake()
    {
        if(lamparaButton != null && radioButton != null && tazaButton != null)
        {
            lamparaButton.onClick.AddListener(() => PressButton(BoxButton.Lampara));
            radioButton.onClick.AddListener(() => PressButton(BoxButton.Radio));
            tazaButton.onClick.AddListener(() => PressButton(BoxButton.Taza));
        }
    }
    private void PressButton(BoxButton buttonName)
    {
        if(currentPuzzle == null){return;}
        currentPuzzle.ButtonPressed(buttonName);
    }
    public void ShowBox(BoxPuzzle boxPuzzle)
    {
        currentPuzzle = boxPuzzle;
        gameObject.SetActive(true);
    }
    public void CloseBox()
    {
        gameObject.SetActive(false);
    }
    
}
