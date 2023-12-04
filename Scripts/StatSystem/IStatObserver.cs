using UnityEngine;

namespace EndlessWinter.Stat
{
    public interface IStatObserver
    {
        public void OnNotify(StatType type, float param);
    }
}
