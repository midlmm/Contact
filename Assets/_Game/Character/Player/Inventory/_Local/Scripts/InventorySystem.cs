using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InventorySystem : MonoBehaviour
{
    [SerializeField] private ItemsCollectionData _itemsCollectionData;

    [SerializeField] private InventorySlotView _prefabInventorySlotView;
    [SerializeField] private Transform _content;
    [SerializeField] private InventoryItem _inventoryItem;

    private List<InventorySlotPresent> _inventorySlotPresents = new List<InventorySlotPresent>();
    private Inventory _inventory;

    private void Start()
    {
        var inventoryConfig = new InventoryConfig { InventorySize = new Vector2Int(5,5)};
        _inventory = new Inventory(inventoryConfig);

        _inventory.ItemAdded += ItemAdd;
        _inventory.ItemRemoved += ItemRemove;

        CreateInventorySlots();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1)) _inventory.Add(_itemsCollectionData.ItemDatas[0].ItemsConfig, Random.Range(1,4));
        if (Input.GetKeyDown(KeyCode.Alpha2)) _inventory.Add(_itemsCollectionData.ItemDatas[1].ItemsConfig, Random.Range(1,7));
    }

    private void CreateInventorySlots()
    {
        var inventorySlots = _inventory.Slots;

        foreach (var slot in inventorySlots)
        {
            var inventorySlotView = Instantiate(_prefabInventorySlotView, _content);
            var inventorySlotPresent = new InventorySlotPresent(inventorySlotView, slot, _inventoryItem);

            inventorySlotPresent.Initialization();
            inventorySlotPresent.OnDropped += Drop;

            _inventorySlotPresents.Add(inventorySlotPresent);
        }
    }

    private void Drop(InventorySlot at, InventorySlot to, PointerEventData.InputButton inputButton)
    {
        var newAt = new InventorySlot { ItemConfig = _inventoryItem.InventorySlot.ItemConfig, Count = _inventoryItem.InventorySlot.Count };
        var newTo = new InventorySlot { ItemConfig = to.ItemConfig, Count = to.Count };

        var isLeftDrag = false;

        switch (inputButton)
        {
            case PointerEventData.InputButton.Left:
                isLeftDrag = true;
                break;
            case PointerEventData.InputButton.Right:
                isLeftDrag = false;
                break;
            case PointerEventData.InputButton.Middle:
                isLeftDrag = true;
                break;
            default:
                isLeftDrag = true;
                break;
        }

        if (isLeftDrag) // if dragging a left mouse button
        {
            if (to.Count > 0 && at.ItemConfig == to.ItemConfig) // if next slot is not empty and items type are equal
            {
                _inventory.Remove(at.GetSlotCoordinates(_inventory), at.ItemConfig, at.Count, false);
                _inventory.Remove(to.GetSlotCoordinates(_inventory), to.ItemConfig, to.Count, false);
                _inventory.Add(to.GetSlotCoordinates(_inventory), newAt.ItemConfig, newAt.Count + newTo.Count);
            }
            else if (to.Count > 0 && at.ItemConfig != to.ItemConfig) // if next slot is not empty but types are different
            {
                _inventory.Remove(at.GetSlotCoordinates(_inventory), at.ItemConfig, at.Count, false);
                _inventory.Remove(to.GetSlotCoordinates(_inventory), to.ItemConfig, to.Count, false);
                _inventory.Add(to.GetSlotCoordinates(_inventory), newAt.ItemConfig, newAt.Count);
                _inventory.Add(at.GetSlotCoordinates(_inventory), to.ItemConfig, newTo.Count);
            }
            else if (to.Count == 0) // if next is empty
            {
                _inventory.Remove(at.GetSlotCoordinates(_inventory), at.ItemConfig, at.Count, false);
                _inventory.Add(to.GetSlotCoordinates(_inventory), newAt.ItemConfig, newAt.Count);
            }
        }
        else // if dragging a right mouse button
        {
            if (to.Count > 0 && at.ItemConfig == to.ItemConfig) // if next slot is not empty and items type are equal
            {
                _inventory.Remove(to.GetSlotCoordinates(_inventory), to.ItemConfig, to.Count, false);
                _inventory.Add(to.GetSlotCoordinates(_inventory), newAt.ItemConfig, newAt.Count + newTo.Count);
            }
            else if (to.Count > 0 && at.ItemConfig != to.ItemConfig) // if next slot is not empty but types are different
            {
                _inventory.Remove(to.GetSlotCoordinates(_inventory), to.ItemConfig, to.Count, false);
                _inventory.Add(to.GetSlotCoordinates(_inventory), newAt.ItemConfig, newAt.Count);
                _inventory.Add(to.ItemConfig, to.Count);
            }
            else if (to.Count == 0) // if next is empty
            {
                _inventory.Add(to.GetSlotCoordinates(_inventory), newAt.ItemConfig, newAt.Count);
            }
        }
    }

    private void ItemAdd(InventoryEventArguments arguments)
    {
        var slot = _inventorySlotPresents[arguments.InventorySlotCoordinates.x + _inventory.Size.x * arguments.InventorySlotCoordinates.y];
        slot.UpdateSlot(arguments.itemConfig, arguments.Count);
    }

    private void ItemRemove(InventoryEventArguments arguments)
    {
        var slot = _inventorySlotPresents[arguments.InventorySlotCoordinates.x + _inventory.Size.x * arguments.InventorySlotCoordinates.y];
        slot.UpdateSlot(arguments.itemConfig, arguments.Count);
    }
}
