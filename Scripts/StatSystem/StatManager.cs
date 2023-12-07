using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

namespace EndlessWinter.Stat
{
    public class StatManager : Singleton<StatManager>, IStatObserver
    {
        #region Variables
        [SerializeField] private List<Stat> m_StatList = new List<Stat>();
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
            UIStatUpdater uiStatUpdater = UIManager.Instance.UIStatUpdater(type);
            uiStatUpdater?.UpdateUI(param);
        }
        #endregion

    }
}

