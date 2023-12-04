using System.Collections.Generic;
using UnityEngine;
namespace EndlessWinter.Stat
{
    public class StatManager : Singleton<StatManager>, IStatObserver
    {
        #region Variables
        [SerializeField] private List<Stat> m_StatList = new List<Stat>();
        private float m_RefPreviousValue; 
        #endregion
        private void Start() => m_StatList.ForEach(r => r.AttachObserver(this));
        #region Funcs
        public T GetStat<T>() where T : Stat
        {
            foreach (Stat stat in m_StatList)
                if (stat is T typedStat)
                    return typedStat;

            return null;
        }
        public void OnNotify(StatType type, float param)
        {
            var maxValue = m_StatList.Find(r => r.Type == type)?.MaxValue ?? 0.0f;
            UIStatUpdater uiStatUpdater = UIManager.Instance.UIStatUpdater(type);
            if (m_RefPreviousValue != param)
            {
                uiStatUpdater?.UpdateUI(param);
                m_RefPreviousValue = param;
                uiStatUpdater.DisplayCount = 3;
            }
        }
        #endregion

    }
}

