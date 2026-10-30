using UnityEngine;

[CreateAssetMenu(fileName = "NoteData", menuName = "Scriptable Objects/NoteData")]
public class NoteData : ScriptableObject
{
    public string title;
    [TextArea(3, 10)]
    public string content;

    [Header("Configuración visual")]
    [SerializeField] private Sprite backgroundSprite;

    [SerializeField]
    private Vector2 backgroundSize = new Vector2(600f, 800f);

    [Header("Distribución del texto")]
    [SerializeField]
    private Vector2 textPosition = Vector2.zero;

    [SerializeField]
    private Vector2 textSize = new Vector2(450f, 600f);

    [SerializeField] private float fontSize = 30f;

    [Header("Espaciado del texto")]
    [SerializeField] private float characterSpacing = 0f;
    [SerializeField] private float wordSpacing = 0f;
    [SerializeField] private float lineSpacing = 0f;
    [SerializeField] private float paragraphSpacing = 0f;

    public float CharacterSpacing => characterSpacing;
    public float WordSpacing => wordSpacing;
    public float LineSpacing => lineSpacing;
    public float ParagraphSpacing => paragraphSpacing;


    // Propiedades públicas de solo lectura
    public Sprite BackgroundSprite => backgroundSprite;
    public Vector2 BackgroundSize => backgroundSize;
    public Vector2 TextPosition => textPosition;
    public Vector2 TextSize => textSize;
    public float FontSize => fontSize;

}
