using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour, IDropHandler
{
    [SerializeField] private Image image;
    [SerializeField] private Color unselectedColor;
    [SerializeField] private Color selectedColor;

    public void OnSelected()
    {
        image.color = selectedColor;
    }

    public void OnUnselected()
    {
        image.color = unselectedColor;
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (transform.childCount == 0)
        {
            var inventoryItem = eventData.pointerDrag.GetComponent<InventoryItem>();
            if (inventoryItem != null)
            {
                inventoryItem.ParentAfterDrag = transform;
            }
        }
    }
}
