
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NoteUI : MonoBehaviour
{
    [Header("Referencias")]
    [SerializeField] private TMP_Text contentText;
    [SerializeField] private Image noteBackground;

    public void ShowNote(NoteData noteData)
    {
        if (noteData == null) return;

        contentText.text = noteData.content;

        ApplyVisuals(noteData);

        gameObject.SetActive(true);
    }

    public void CloseNote()
    {
        gameObject.SetActive(false);
    }

    private void ApplyVisuals(NoteData data)
    {
        // Configurar imagen de fondo.
        noteBackground.sprite = data.BackgroundSprite;

        RectTransform backgroundRect = noteBackground.rectTransform;

        backgroundRect.sizeDelta = data.BackgroundSize;

        // Configurar distribución del texto.
        RectTransform textRect =
            contentText.rectTransform;

        textRect.anchorMin = new Vector2(0.5f, 0.5f);
        textRect.anchorMax = new Vector2(0.5f, 0.5f);
        textRect.pivot = new Vector2(0.5f, 0.5f);

        textRect.anchoredPosition = data.TextPosition;
        textRect.sizeDelta = data.TextSize;

        // Configurar tamaño de fuente.
        contentText.enableAutoSizing = false;
        contentText.fontSize = data.FontSize;

        // Configurar espaciado del texto.
        contentText.characterSpacing = data.CharacterSpacing;
        contentText.wordSpacing = data.WordSpacing;
        contentText.lineSpacing = data.LineSpacing;
        contentText.paragraphSpacing = data.ParagraphSpacing;

    }
}
