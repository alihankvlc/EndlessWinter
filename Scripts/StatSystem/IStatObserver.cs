namespace EndlessWinter.Stat
{
    using UnityEngine;

    public interface IStatObserver
    {
        public void OnNotify(StatType type, float param);
    }
}
