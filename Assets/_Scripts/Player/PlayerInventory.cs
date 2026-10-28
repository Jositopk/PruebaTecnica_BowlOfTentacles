
using System;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    [Header("Referencias")]
    [SerializeField] private Transform hand;
    [SerializeField] private Camera playerCamera;

    [Header("Soltar objetos")]
    [SerializeField] private float dropDistance = 3f;
    [SerializeField] private float dropOffset = 0.2f;
    [SerializeField] private LayerMask dropLayers = ~0;

    public ItemData CurrentItem { get; private set; }

    // Referencia al objeto fisico recogido.
    private GameObject heldObject;
    private Collider[] heldColliders;

    public event Action<ItemData> OnItemPickedUp;
    public event Action<ItemData> OnItemDropped;

    public bool TryPickUp(ItemData item, GameObject worldObject)
    {
        if (item == null || worldObject == null ||
            CurrentItem != null || hand == null)
            return false;

        CurrentItem = item;
        heldObject = worldObject;

        // Desactivar colisiones mientras lo llevamos.
        heldColliders = worldObject.GetComponentsInChildren<Collider>(true);

        foreach (Collider col in heldColliders)
            col.enabled = false;

        // Colocar el objeto en la mano.
        heldObject.transform.SetParent(hand, false);
        heldObject.transform.localPosition = Vector3.zero;
        heldObject.transform.localRotation = Quaternion.identity;

        OnItemPickedUp?.Invoke(item);

        return true;
    }

    public bool DropItem()
    {
        if (CurrentItem == null || heldObject == null ||
            playerCamera == null)
            return false;

        Transform cam = playerCamera.transform;

        // Buscar donde esta apuntando el jugador.
        Vector3 dropPosition = cam.position + cam.forward * dropDistance;

        if (Physics.Raycast(cam.position, cam.forward,out RaycastHit hit, dropDistance,dropLayers, QueryTriggerInteraction.Ignore))
        {
            dropPosition = hit.point + hit.normal * dropOffset;
        }

        // Separar el objeto de la mano.
        heldObject.transform.SetParent(null, true);
        heldObject.transform.position = dropPosition;

        // Recuperar colisiones.
        foreach (Collider col in heldColliders)
        {
            if (col != null)
                col.enabled = true;
        }

        ItemData droppedItem = CurrentItem;

        CurrentItem = null;
        heldObject = null;
        heldColliders = null;

        OnItemDropped?.Invoke(droppedItem);

        return true;
    }

    public bool HasItem(ItemData item)
    {
        return item != null && CurrentItem == item;
    }
}
