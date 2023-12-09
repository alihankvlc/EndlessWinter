namespace EndlessWinter.Manager
{
    using DG.Tweening;
    using EndlessWinter.Stat;
    using System.Collections.Generic;
    using System.Linq;
    using TMPro;
    using UnityEngine;

    public class UIManager : Singleton<UIManager>
    {
        #region Variables
        [SerializeField] private List<UIStatUpdater> m_UIStatList = new List<UIStatUpdater>();
        [SerializeField] private TextMeshProUGUI m_TMP_Time;
        [SerializeField] private TextMeshProUGUI m_TMP_Temp;
        [SerializeField] private TextMeshProUGUI m_TMP_Inform;
        private Dictionary<StatType, UIStatUpdater> m_UIStatDataDic = new Dictionary<StatType, UIStatUpdater>();

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
        #region Funcs
        protected override void Awake()
            => DOTween.SetTweensCapacity(5000, 500);

        private void Start()
            => m_UIStatDataDic = m_UIStatList.ToDictionary(uiUpdater => uiUpdater.Stat.Type);

        public UIStatUpdater UIStatUpdater(StatType type)
        {
            if (m_UIStatDataDic.TryGetValue(type, out UIStatUpdater stat))
                return stat;

            return null;
        }
        #endregion
    }
}

