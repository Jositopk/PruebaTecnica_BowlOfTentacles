using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections.Generic;

public class BoxUI : MonoBehaviour
{
    [Serializable]
    private class SymbolSprite
    {
        public BoxButton symbol;
        public Sprite sprite;
    }

    //referencia a los 3 botones
    [SerializeField] private Button lamparaButton;
    [SerializeField] private Button radioButton;
    [SerializeField] private Button tazaButton;
    [SerializeField] private Button clearButton;
    private bool messageShown = false;
    private BoxPuzzle currentPuzzle;

    [Header("Secuencia visual")]
    [SerializeField] private Image[] slotImages;
    [SerializeField] private List<SymbolSprite> symbolSprites = new();
    public void Awake()
    {
        if (clearButton != null)
            clearButton.onClick.AddListener(ClearAttempt);
        if (lamparaButton != null && radioButton != null && tazaButton != null)
        {
            lamparaButton.onClick.AddListener(() => PressButton(BoxButton.Lampara));
            radioButton.onClick.AddListener(() => PressButton(BoxButton.Radio));
            tazaButton.onClick.AddListener(() => PressButton(BoxButton.Taza));
        }

    }
    private void PressButton(BoxButton buttonName)
    {
        if (currentPuzzle == null) { return; }
        currentPuzzle.ButtonPressed(buttonName);
    }
    public void ShowBox(BoxPuzzle boxPuzzle)
    {
        currentPuzzle = boxPuzzle;
        currentPuzzle.OnSequenceChanged += RefreshSlots;
        currentPuzzle.OnAttemptFailed += ShowFailure;
        gameObject.SetActive(true);
        RefreshSlots();
        if (!messageShown)
        {
            messageShown = true;
            UIManager.Instance.ShowInformation("Mamá guardaba aquí la llave de repuesto");
        }
    }
    public void CloseBox()
    {
        if (currentPuzzle != null)
        {
            currentPuzzle.OnSequenceChanged -= RefreshSlots;
            currentPuzzle.OnAttemptFailed -= ShowFailure;
            currentPuzzle = null;
        }

        gameObject.SetActive(false);
    }
    private void RefreshSlots()
    {
        for (int i = 0; i < slotImages.Length; i++)
        {
            Image image = slotImages[i];

            bool occupied = i < currentPuzzle.CurrentSequence.Count;

            image.sprite = occupied
                ? GetSprite(currentPuzzle.CurrentSequence[i])
                : null;

            image.enabled = image.sprite != null;
        }
        if (clearButton != null)
        {
            clearButton.interactable =
                currentPuzzle != null &&
                !currentPuzzle.IsSolved &&
                !currentPuzzle.IsResolving &&
                currentPuzzle.CurrentSequence.Count > 0;
        }
    }
    private void ShowFailure()
    {
        UIManager.Instance?.ShowInformation(
            "No era así su rutina"
        );
    }
    private void ClearAttempt()
    {
        currentPuzzle?.ResetPuzzle();
    }

    private Sprite GetSprite(BoxButton symbol)
    {
        foreach (var item in symbolSprites)
        {
            if (item.symbol == symbol)
                return item.sprite;
        }

        return null;
    }
}
