using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryItemView : MonoBehaviour
{
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
}
