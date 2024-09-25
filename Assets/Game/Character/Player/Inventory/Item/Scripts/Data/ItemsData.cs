using UnityEngine;

[CreateAssetMenu(menuName = "Datas/Items Data")]
public class ItemsData : ScriptableObject
{
    public ItemConfig[] ItemsConfigs => _itemConfigs;

    [SerializeField] private ItemConfig[] _itemConfigs;
}
