using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InventorySlotPresent 
{
    public Action<InventorySlot, InventorySlot, bool> OnDropped;

    private InventorySlotView _inventorySlotView;
    private InventorySlot _inventorySlot;
    private InventoryItem _inventoryItem;

    private bool _isDragging;
    private bool _isLeftDrag;

    public InventorySlotPresent(InventorySlotView inventorySlotView, InventorySlot inventorySlot, InventoryItem inventoryItem)
    {
        _inventorySlotView = inventorySlotView;
        _inventorySlot = inventorySlot;
        _inventoryItem = inventoryItem;

        DisplaySlot();

        _inventorySlotView.OnBeginDraggedLeft += OnBeginDragLeft;
        _inventorySlotView.OnBeginDraggedRight += OnBeginDragRight;
        _inventorySlotView.OnDragged += OnDrag;
        _inventorySlotView.OnEndDragged += OnEndDrag;
        _inventorySlotView.OnDropped += OnDrop;
    }

    public void OnDestroy()
    {
        _inventorySlotView.OnBeginDraggedLeft -= OnBeginDragLeft;
        _inventorySlotView.OnBeginDraggedRight -= OnBeginDragRight;
        _inventorySlotView.OnDragged -= OnDrag;
        _inventorySlotView.OnEndDragged -= OnEndDrag;
        _inventorySlotView.OnDropped -= OnDrop;
    }

    public void UpdateSlot(ItemConfig itemConfig, int count)
    {
        _inventorySlot.ItemConfig = itemConfig;
        _inventorySlot.Count = count;

        DisplaySlot();
    }

    private void DisplaySlot()
    {
        var countText = "";
        if (_inventorySlot.Count > 1) countText = _inventorySlot.Count.ToString();
        _inventorySlotView.DisplayInfo(_inventorySlot.ItemConfig.Sprite, countText);
    }

    private void OnBeginDragLeft()
    {
        _isLeftDrag = true;
        OnBeginDrag();
        _inventorySlotView.DisplayInfo(null, "");
    }

    private void OnBeginDragRight()
    {
        if (_inventorySlot.Count < 2)
        {
            OnBeginDragLeft();
            return;
        }

        _inventorySlot.Count /= 2;
        DisplaySlot();

        _isLeftDrag = false;
        OnBeginDrag();
    }

    private void OnBeginDrag()
    {
        if (_inventorySlot.IsEmpty()) return;

        DisplaySlot();

        _inventoryItem.Initialized(_inventorySlot, _isLeftDrag);
        _inventoryItem.SetAlpha(1.0f);

        _isDragging = true;
    }

    private void OnDrag()
    {
        if (!_isDragging) return;

        _inventoryItem.transform.position = Input.mousePosition;
    }

    private void OnEndDrag()
    {
        if (!_isDragging) return;

        _inventoryItem.SetAlpha(0f);

        _isDragging = false;
    }

    private void OnDrop()
    {
        if (_inventoryItem.InventorySlot == null) return;

        OnDropped?.Invoke(_inventoryItem.InventorySlot, _inventorySlot, _inventoryItem.IsLeftDrag);
        _inventoryItem.ClearInventorySlot();
    }
}
