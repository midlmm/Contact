using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Inventory 
{
    public event Action<InventoryEventArguments> ItemAdded;
    public event Action<InventoryEventArguments> ItemRemoved;
    public event Action<ItemConfig, int> ItemDroped;

    public List<InventorySlotConfig> Slots;
    public Vector2Int Size;

    public Inventory(InventoryConfig inventoryConfig)
    {
        Size = inventoryConfig.InventorySize;

        var size = Size;
        Slots = new List<InventorySlotConfig>(size.x * size.y);
        for (int i = 0; i < Size.x * Size.y; i++)
        {
            Slots.Add(new InventorySlotConfig());
        }
    }
    public void InvokeItemAdded(InventoryEventArguments inventoryEventArguments)
    {
        ItemAdded?.Invoke(inventoryEventArguments);
    }

    public void InvokeItemRemoved(InventoryEventArguments inventoryEventArguments)
    {
        ItemRemoved?.Invoke(inventoryEventArguments);
    }

    public void InvokeItemDroped(ItemConfig itemConfig, int count)
    {
        ItemDroped?.Invoke(itemConfig, count);
    }
}
