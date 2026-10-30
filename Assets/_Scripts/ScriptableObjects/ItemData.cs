using UnityEngine;

[CreateAssetMenu(fileName = "ItemData", menuName = "Scriptable Objects/ItemData")]
public class ItemData : ScriptableObject
{
    public string itemName;
    public bool consumable;
    [Header("Configuración en mano")]
    [SerializeField] private Vector3 holdPosition = Vector3.zero;
    [SerializeField] private Vector3 holdRotation = Vector3.zero;

    public Vector3 HoldPosition => holdPosition;
    public Vector3 HoldRotation => holdRotation;
}
