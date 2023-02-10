using UnityEngine;
using System.Collections.Generic;

namespace Features.Inventory.Items
{
    [CreateAssetMenu(fileName = nameof(ItemConfigDataSource), menuName = "Configs/" + nameof(ItemConfigDataSource))]
    internal class ItemConfigDataSource : ScriptableObject    // to have a specific list of items that are needed now
    {
        [SerializeField] private ItemConfig[] _itemConfigs;

        public IReadOnlyList<ItemConfig> ItemConfigs => _itemConfigs;
    }
}
