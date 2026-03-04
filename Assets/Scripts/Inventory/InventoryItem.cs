using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField] private Image image;
    [SerializeField] private GameObject counterGameObject;
    [SerializeField] private TMP_Text counterText;

    public Transform ParentAfterDrag { get; set; }
    public int Counter { get; set; }
    public InventoryItemData Data { get; set; }

    public void Init(InventoryItemData data)
    {
        if (data == null)
        {
            return;
        }

        image.sprite = data.Sprite;
        Data = data;
        Counter = 1;
        UpdateCounter();
    }

    public void UpdateCounter()
    {
        counterText.text = $"{Counter}";
        bool isCounterVisible = Counter > 1;
        counterGameObject.SetActive(isCounterVisible);
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
