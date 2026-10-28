using UnityEngine;

public class TransformPositioner : MonoBehaviour
{
    [Header("Referencias")]
    [SerializeField] private Transform target;
    [SerializeField] private Transform destination;

    [ContextMenu("Ejecutar movimiento")]
    public void ApplyPosition()
    {
        if (target == null || destination == null)
        {
            Debug.LogWarning("Faltan referencias en TransformPositioner.");
            return;
        }

        target.SetPositionAndRotation(
            destination.position,
            destination.rotation
        );
    }
}