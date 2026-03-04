using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item List", menuName = "Inventory/Item List")]
public class InventoryData : ScriptableObject
{
    [SerializeField] private List<InventoryItemData> dataList = new List<InventoryItemData>();

    public List<InventoryItemData> DataList => dataList;
}