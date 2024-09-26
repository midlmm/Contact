using UnityEngine;

[CreateAssetMenu(menuName = "Datas/Item Data")]
public class ItemData : ScriptableObject
{
    public ItemConfig ItemsConfig => _itemConfig;

    [SerializeField] private ItemConfig _itemConfig;
}
