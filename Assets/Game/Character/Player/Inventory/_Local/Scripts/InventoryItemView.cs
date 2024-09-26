using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryItemView : MonoBehaviour
{
    [SerializeField] private Image _imageIcon;
    [SerializeField] private TMP_Text _textCount;

    public void DisplayInfo(Sprite icon, string count)
    {
        _imageIcon.sprite = icon;
        _textCount.text = count;
    }
}
