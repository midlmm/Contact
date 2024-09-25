using UnityEngine;

public struct InventoryEventArguments 
{
    public ItemConfig itemConfig { get; }
    public int Count { get; }
    public Vector2Int InventorySlotCoordinates { get; }

    public InventoryEventArguments(ItemConfig itemConfig, int count, Vector2Int inventorySlotCoordinates)
    {
        this.itemConfig = itemConfig;
        Count = count;
        InventorySlotCoordinates = inventorySlotCoordinates;
    }
}
