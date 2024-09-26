using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventorySlotView : MonoBehaviour, IDropHandler, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public Action OnBeginDraggedLeft;
    public Action OnBeginDraggedRight;
    public Action OnDragged;
    public Action OnEndDragged;
    public Action OnDropped;

    [SerializeField] private Image _imageIcon;
    [SerializeField] private TMP_Text _textCount;

    public void DisplayInfo(Sprite icon, string count)
    {
        _imageIcon.sprite = icon;
        _textCount.text = count;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Middle) return;
            if (eventData.button == PointerEventData.InputButton.Left) OnBeginDraggedLeft?.Invoke();
        if (eventData.button == PointerEventData.InputButton.Right) OnBeginDraggedRight?.Invoke();
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
