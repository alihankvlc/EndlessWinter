namespace EndlessWinter.Stat
{
    using System.Collections;
    using System.Collections.Generic;
    using EndlessWinter.Manager;
    using UnityEngine;

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
        public void OnNotify(StatType type, float currentValue)
        {
            UIStatUpdater uiStatUpdater = UIManager.Instance.UIStatUpdater(type);
            uiStatUpdater?.UpdateUI(currentValue);

            if (currentValue == 0 && this != null)
                StartCoroutine(ShowInform(uiStatUpdater.Stat,2.5f));
        }

        // Düzenlenecek...
        private IEnumerator ShowInform(Stat stat, float duration)
        {
            WaitForSeconds delay = new WaitForSeconds(duration);
            UIManager.Instance.TMP_Inform.SetText(stat.Inform);

            AudioSource.PlayClipAtPoint(stat?.InformSoundEffect, FindAnyObjectByType<CharacterController>().center, 1);
            yield return delay;
            UIManager.Instance.TMP_Inform.SetText(""); // duratiodasn sonra şimdilik Null
        }
        #endregion

    }
}

