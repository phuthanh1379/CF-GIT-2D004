using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField] private Image image;

    public Transform ParentAfterDrag { get; set; }
    private InventoryItemData inventoryItemData;

    public void Init(InventoryItemData data)
    {
        if (data == null)
        {
            return;
        }

        image.sprite = data.Sprite;
        inventoryItemData = data;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        image.raycastTarget = false;
        ParentAfterDrag = transform.parent;
        transform.SetParent(transform.root);
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        image.raycastTarget = true;
        transform.SetParent(ParentAfterDrag);
    }
}
