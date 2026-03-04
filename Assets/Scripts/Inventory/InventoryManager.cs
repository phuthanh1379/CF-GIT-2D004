using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    [SerializeField] private InventoryItem itemPrefab;
    [SerializeField] private InventoryData data;
    [SerializeField] private List<InventorySlot> slotList = new List<InventorySlot>();

    [Header("Item Info UI")]
    [SerializeField] private GameObject itemInfoGameObject;
    [SerializeField] private Image itemInfoImage;
    [SerializeField] private TMP_Text itemNameText;
    [SerializeField] private TMP_Text itemStackableText;

    private const int MaxStackItem = 3;
    private const int MaxSelectSlot = 7;
    private int _selectedSlotIndex;

    private void Start()
    {
        itemInfoGameObject.SetActive(false);
    }

    private void Update()
    {
        if (Input.inputString != null)
        {
            bool isInputNumber = int.TryParse(Input.inputString, out int number);
            if (isInputNumber && number > 0 && number <= MaxSelectSlot)
            {
                SelectSlot(number - 1);
            }
        }

        if (Input.GetKeyDown(KeyCode.Return))
        {
            DisplayItemInfo();
        }
    }

    private void SelectSlot(int index)
    {
        if (slotList == null || slotList.Count == 0 || index < 0 || index >= slotList.Count)
        {
            return;
        }

        slotList[_selectedSlotIndex].OnUnselected();
        slotList[index].OnSelected();
        _selectedSlotIndex = index;
    }

    private void DisplayItemInfo()
    {
        if (slotList == null || slotList.Count == 0 || _selectedSlotIndex < 0 || _selectedSlotIndex >= slotList.Count)
        {
            return;
        }

        InventorySlot selectedSlot = slotList[_selectedSlotIndex];
        if (selectedSlot == null)
        {
            return;
        }

        InventoryItem itemInSlot = selectedSlot.GetComponentInChildren<InventoryItem>();
        if (itemInSlot != null && itemInSlot.Data != null)
        {
            itemInfoGameObject.SetActive(true);
            itemInfoImage.sprite = itemInSlot.Data.Sprite;
            itemNameText.text = itemInSlot.Data.Name;
            itemStackableText.text = itemInSlot.Data.Stackable ? "Stackable" : "Unstackable";
        }
    }

    public void AddRandomItem()
    {
        var rnd = new System.Random();
        var index = rnd.Next(0, data.DataList.Count);
        var itemData = data.DataList[index];
        Debug.Log($"Add Item: {itemData.Name}");
        AddItem(itemData);
    }

    private void AddItem(InventoryItemData item)
    {
        for (var i = 0; i < slotList.Count; ++i)
        {
            var slot = slotList[i];
            var itemInSlot = slot.GetComponentInChildren<InventoryItem>();
            if (itemInSlot != null)
            {
                if (item.Equals(itemInSlot.Data) && item.Stackable && 
                    itemInSlot.Counter < MaxStackItem)
                {
                    itemInSlot.Counter++;
                    itemInSlot.UpdateCounter();
                    return;
                }
                else
                {
                    continue;
                }
            }

            SpawnItem(item, slot);
            return;
        }
    }

    private void SpawnItem(InventoryItemData itemData, InventorySlot slot)
    {
        var item = Instantiate(itemPrefab, slot.transform);
        item.Init(itemData);
    }
}
