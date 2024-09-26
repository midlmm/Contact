using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventorySlotView : MonoBehaviour, IDropHandler, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public Action<PointerEventData.InputButton> OnBeginDragged;
    public Action OnDragged;
    public Action OnEndDragged;
    public Action OnDropped;

    [SerializeField] private Image _imageIcon;
    [SerializeField] private TMP_Text _textCount;

    public void DisplayInfo(Sprite icon, int count)
    {
        var countText = "";
        if (count > 1)
            countText = count.ToString();

        _textCount.text = countText;

        if (icon == null)
            _imageIcon.color = new Color(1, 1, 1, 0);
        else
            _imageIcon.color = Color.white;

        _imageIcon.sprite = icon;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        OnBeginDragged?.Invoke(eventData.button);
    }

    public void OnDrag(PointerEventData eventData)
    {
        OnDragged?.Invoke();
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        OnEndDragged?.Invoke();
    }

    public void OnDrop(PointerEventData eventData)
    {
        OnDropped?.Invoke();
    }
}
