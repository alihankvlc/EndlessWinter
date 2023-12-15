namespace EndlessWinter.UI
{
    using DG.Tweening;
    using EndlessWinter.Stat;
    using System.Collections.Generic;
    using System.Linq;
    using TMPro;
    using UnityEngine;

    public class UIManager : Singleton<UIManager>
    {
        #region StatsSystem
        [Header("Stat Variables")]
        [SerializeField] private List<UIStat> m_UIStatList = new List<UIStat>();
        private Dictionary<StatType, UIStat> m_UIStatDataCache = new Dictionary<StatType, UIStat>();
        #endregion
        #region InventorySystem
        [Header("Inventory Variables")]
        [SerializeField] private TextMeshProUGUI m_BackpackCategoryInformText;
        #endregion
        #region Notification
        [Header("Notification Variables")]
        [SerializeField] private TextMeshProUGUI m_TMP_Time;
        [SerializeField] private TextMeshProUGUI m_TMP_Temp;
        [SerializeField] private TextMeshProUGUI m_TMP_Inform;
        #endregion
        #region Property
        public TextMeshProUGUI TMP_Time
        {
            get => m_TMP_Time;
            set => m_TMP_Time = value;
        }
        public TextMeshProUGUI TMP_Temp
        {
            get => m_TMP_Temp;
            set => m_TMP_Temp = value;
        }
        public TextMeshProUGUI TMP_Inform
        {
            get => m_TMP_Inform;
            set => m_TMP_Inform = value;
        }
        #endregion
        protected override void Awake()
        {
            DOTween.SetTweensCapacity(5000, 500);

            EventManager.Instance.Subscribe<string>("UpdateBackpackCategoryText", E_UpdateItemCategoryText);
            EventManager.Instance.Subscribe<ItemCategoryType>("IdentifyItemType", E_IdentifyItemType);
        }

        private void Start()
        {
            m_UIStatDataCache = m_UIStatList.ToDictionary(uiUpdater => uiUpdater.Stat.Type);
        }
        #region UIInventory
        public void E_IdentifyItemType(ItemCategoryType type)
        {
            m_BackpackCategoryInformText.SetText(type.ToString());
        }
        public void E_UpdateItemCategoryText(string param)
            => m_BackpackCategoryInformText.SetText(param);
        #endregion
        #region UIStat
        public UIStat UIStatUpdater(StatType type)
        {
            if (m_UIStatDataCache.TryGetValue(type, out UIStat stat))
                return stat;

            return null;
        }
        #endregion
        public void OnDisable()
        {
            EventManager.Instance.Unsubscribe<string>("UpdateBackpackCategoryText", E_UpdateItemCategoryText);
            EventManager.Instance.Unsubscribe<ItemCategoryType>("IdentifyItemType", E_IdentifyItemType);
        }
    }
}

