using UnityEngine;

[CreateAssetMenu(menuName = "Datas/Items Collection Data")]
public class ItemsCollectionData : ScriptableObject
{
    public ItemData[] ItemDatas => _itemDatas;

    [SerializeField] private ItemData[] _itemDatas;
}