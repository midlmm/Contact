using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InventorySystem : MonoBehaviour
{
    [SerializeField] private ItemsData _itemsData;

    [SerializeField] private GameObject _prefabInventorySlotView;
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
        if (Input.GetKeyDown(KeyCode.Alpha1)) _inventory.Add(_itemsData.ItemsConfigs[0], Random.Range(1,4));
        if (Input.GetKeyDown(KeyCode.Alpha2)) _inventory.Add(_itemsData.ItemsConfigs[1], Random.Range(1,7));
    }

    private void CreateInventorySlots()
    {
        var inventorySlots = _inventory.Slots;

        foreach (var slot in inventorySlots)
        {
            var inventorySlotView = Instantiate(_prefabInventorySlotView, _content).GetComponent<InventorySlotView>();
            var inventorySlotPresent = new InventorySlotPresent(inventorySlotView, slot, _inventoryItem);

            inventorySlotPresent.OnDropped += Drop;

            _inventorySlotPresents.Add(inventorySlotPresent);
        }
    }

    private void Drop(InventorySlot at, InventorySlot to, bool isLeftDrag)
    {
        var newAt = new InventorySlot { ItemConfig = at.ItemConfig, Count = at.Count };
        var newTo = new InventorySlot { ItemConfig = to.ItemConfig, Count = to.Count };

        if(isLeftDrag)
        {
            if (to.Count > 0 && at.ItemConfig == to.ItemConfig)
            {
                _inventory.Remove(at.GetSlotCoordinates(_inventory), at.ItemConfig, at.Count, false);
                _inventory.Remove(to.GetSlotCoordinates(_inventory), to.ItemConfig, to.Count, false);
                _inventory.Add(to.GetSlotCoordinates(_inventory), newAt.ItemConfig, newAt.Count + newTo.Count);
            }
            else if (to.Count > 0 && at.ItemConfig != to.ItemConfig)
            {
                _inventory.Remove(at.GetSlotCoordinates(_inventory), at.ItemConfig, at.Count, false);
                _inventory.Remove(to.GetSlotCoordinates(_inventory), to.ItemConfig, to.Count, false);
                _inventory.Add(to.GetSlotCoordinates(_inventory), newAt.ItemConfig, newAt.Count);
                _inventory.Add(at.GetSlotCoordinates(_inventory), newTo.ItemConfig, newTo.Count);
            }
            else if (to.Count == 0)
            {
                _inventory.Remove(at.GetSlotCoordinates(_inventory), at.ItemConfig, at.Count, false);
                _inventory.Add(to.GetSlotCoordinates(_inventory), newAt.ItemConfig, newAt.Count);
            }
        }
        else
        {
            if (to.Count > 0 && at.ItemConfig == to.ItemConfig)
            {
                _inventory.Remove(to.GetSlotCoordinates(_inventory), to.ItemConfig, to.Count, false);
                _inventory.Add(to.GetSlotCoordinates(_inventory), newAt.ItemConfig, newAt.Count + newTo.Count);
            }
            else if (to.Count > 0 && at.ItemConfig != to.ItemConfig)
            {
                _inventory.Remove(to.GetSlotCoordinates(_inventory), to.ItemConfig, to.Count, false);
                _inventory.Add(to.GetSlotCoordinates(_inventory), newAt.ItemConfig, newAt.Count);
                _inventory.Add(newTo.ItemConfig, newTo.Count);
            }
            else if (to.Count == 0)
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
