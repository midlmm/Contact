using UnityEngine;
using UnityEngine.EventSystems;

public class InventoryItem : MonoBehaviour
{
    public InventorySlot InventorySlot => _inventorySlot;
    public PointerEventData.InputButton InputButton { get; private set; }

    [SerializeField] private InventoryItemView _inventoryItemView;
    [SerializeField] private CanvasGroup _canvasGroup;

    private InventorySlot _inventorySlot;

    public void Initialized(InventorySlot inventorySlot, PointerEventData.InputButton inputButton)
    {
        InputButton = inputButton;
        _inventorySlot = inventorySlot;

        _inventoryItemView.DisplayInfo(inventorySlot.ItemConfig.Sprite, inventorySlot.Count);
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
