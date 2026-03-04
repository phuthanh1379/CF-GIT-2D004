using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    [SerializeField] private InventoryItem itemPrefab;
    [SerializeField] private InventoryData data;
    [SerializeField] private List<InventorySlot> slotList = new List<InventorySlot>();

    public void AddRandomItem()
    {
        var rnd = new System.Random();
        var index = rnd.Next(0, data.DataList.Count);
        var itemData = data.DataList[index];
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
                continue;
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
