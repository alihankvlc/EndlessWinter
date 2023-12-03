using UnityEngine;

namespace Player.Stat
{
    public interface IStatObserver
    {
        public void OnNotify(StatType type, float param);
    }
}
