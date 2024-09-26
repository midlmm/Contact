using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryItem : MonoBehaviour
{
    public InventorySlot InventorySlot => _inventorySlot;
    public bool IsLeftDrag => _isLeftDrag;

    [SerializeField] private InventoryItemView _inventoryItemView;
    [SerializeField] private CanvasGroup _canvasGroup;

    private InventorySlot _inventorySlot;
    private bool _isLeftDrag;

    public void Initialized(InventorySlot inventorySlot, bool isLeftDrag)
    {
        _inventorySlot = inventorySlot;
        _isLeftDrag = isLeftDrag;

        var countText = "";
        if (inventorySlot.Count > 1) countText = inventorySlot.Count.ToString(); 
        _inventoryItemView.DisplayInfo(inventorySlot.ItemConfig.Sprite, countText);
    }

    public void SetAlpha(float alpha)
    {
        _canvasGroup.alpha = alpha;
    }

    public void ClearInventorySlot()
    {
        _inventorySlot = null;
    }
}
