namespace EndlessWinter.Item
{
    using System.Collections.Generic;
    using System.Linq;
    using UnityEngine;

    public class ItemDatabase
    {
        [SerializeField] private List<Item> m_Data;

        private Dictionary<int, Item> m_ItemCache;

        public ItemDatabase()
        {
            m_Data = new List<Item>();
            m_ItemCache = new Dictionary<int, Item>();

            InitializeDataCache();
        }
        public void InitializeDataCache() => m_ItemCache = m_Data.ToDictionary(r => r.Id);
        public T GetItemData<T>(int id) where T : Item
        {
            return m_ItemCache != null && m_ItemCache.TryGetValue(id, out Item item) && item is T typedItem
                ? typedItem
                : null;
        }
    }
}