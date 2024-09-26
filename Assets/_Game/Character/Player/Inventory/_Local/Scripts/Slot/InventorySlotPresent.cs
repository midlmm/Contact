using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class InventorySlotPresent 
{
    public Action<InventorySlot, InventorySlot, PointerEventData.InputButton> OnDropped;

    private InventorySlotView _inventorySlotView;
    private InventorySlot _inventorySlot;
    private InventoryItem _inventoryItem;

    private bool _isDragging;

    public InventorySlotPresent(InventorySlotView inventorySlotView, InventorySlot inventorySlot, InventoryItem inventoryItem)
    {
        _inventorySlotView = inventorySlotView;
        _inventorySlot = inventorySlot;
        _inventoryItem = inventoryItem;

        _inventorySlotView.OnBeginDragged += OnBeginDrag;
        _inventorySlotView.OnDragged += OnDrag;
        _inventorySlotView.OnEndDragged += OnEndDrag;
        _inventorySlotView.OnDropped += OnDrop;
    }

    public void Initialization()
    {
        _inventorySlotView.DisplayInfo(_inventorySlot.ItemConfig.Sprite, _inventorySlot.Count);
    }

    public void OnDestroy()
    {
        _inventorySlotView.OnBeginDragged -= OnBeginDrag;
        _inventorySlotView.OnDragged -= OnDrag;
        _inventorySlotView.OnEndDragged -= OnEndDrag;
        _inventorySlotView.OnDropped -= OnDrop;
    }

    public void UpdateSlot(ItemConfig itemConfig, int count)
    {
        _inventorySlot.ItemConfig = itemConfig;
        _inventorySlot.Count = count;

        _inventorySlotView.DisplayInfo(_inventorySlot.ItemConfig.Sprite, _inventorySlot.Count);
    }

    private InventorySlot OnBeginDragLeft()
    {
        if (_inventorySlot.IsEmpty())
            return null;

        _inventorySlotView.DisplayInfo(null, 0);

        return _inventorySlot;
    }

    private InventorySlot OnBeginDragRight()
    {
        if (_inventorySlot.IsEmpty())
            return null;

        if (_inventorySlot.Count < 2)
            return OnBeginDragLeft();

        var countVar = ((float)_inventorySlot.Count) / 2;

        var countA = Mathf.CeilToInt(countVar);
        var countB = Mathf.FloorToInt(countVar);

        _inventorySlot.Count = countA;

        _inventorySlotView.DisplayInfo(_inventorySlot.ItemConfig.Sprite, _inventorySlot.Count);

        return new InventorySlot { ItemConfig = _inventorySlot.ItemConfig, Count = countB};
    }

    private void OnBeginDrag(PointerEventData.InputButton inputButton)
    {
        var inventorySlot = new InventorySlot();

        switch (inputButton)
        {
            case PointerEventData.InputButton.Left:
                inventorySlot = OnBeginDragLeft();
                break;
            case PointerEventData.InputButton.Right:
                inventorySlot = OnBeginDragRight();
                break;
            case PointerEventData.InputButton.Middle:
                inventorySlot = OnBeginDragLeft();
                break;
            default:
                inventorySlot = OnBeginDragLeft();
                break;
        }

        if (inventorySlot == null) 
            return;

        _inventorySlotView.DisplayInfo(_inventorySlot.ItemConfig.Sprite, _inventorySlot.Count);

        _inventoryItem.Initialized(inventorySlot, inputButton);
        _inventoryItem.SetAlpha(1.0f);

        _isDragging = true;
    }

    private void OnDrag()
    {
        if (!_isDragging)
            return;

        _inventoryItem.transform.position = Input.mousePosition;
    }

    private void OnEndDrag()
    {
        if (!_isDragging)
            return;

        _inventoryItem.SetAlpha(0f);
        _inventorySlotView.DisplayInfo(_inventorySlot.ItemConfig.Sprite, _inventorySlot.Count);

        _isDragging = false;
    }

    private void OnDrop()
    {
        if (_inventoryItem.InventorySlot == null)
            return;

        OnDropped?.Invoke(_inventoryItem.InventorySlot, _inventorySlot, _inventoryItem.InputButton);
        _inventoryItem.ClearInventorySlot();
    }
}
