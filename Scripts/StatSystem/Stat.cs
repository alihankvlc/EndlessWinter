namespace EndlessWinter.Stat
{
    using System.Collections.Generic;
    using UnityEngine;
    public abstract class Stat : ScriptableObject
    {
        #region Variables
        [Header("General Settings")]
        [SerializeField] internal StatType Type = StatType.None;
        [SerializeField] internal float CurrentValue = 100;
        [Range(5, 500)]
        [SerializeField] internal int MaxValue = 100;
        [SerializeField] internal Color CustomColor = Color.white;
        [SerializeField] internal Color ThresholdColor = Color.red;
        [SerializeField,] internal string Inform = "Null";
        [SerializeField] internal AudioClip InformSoundEffect;
        [Header("Increase & Decrease Arrow Settings")]
        [SerializeField] internal float FadeInDuration = 0.35f;
        [SerializeField] internal float FadeOutDuration = 0.35f;
        [SerializeField] internal float Delay = 0.0f;

        [HideInInspector] public float StatChangedAmount;
        #endregion
        #region Virtual Property
        internal virtual float Modify
        {
            get => CurrentValue;
            set
            {
                float previousValue = CurrentValue;
                float newValue = Mathf.Clamp(value, 0, MaxValue);

                if (newValue != CurrentValue)
                {
                    bool isIncrease = newValue > previousValue;
                    CurrentValue = newValue;

                    StatChangedAmount = isIncrease ? newValue - previousValue : (previousValue - newValue) * -1;
                    NotifyObservers();
                }
            }
        }

        #endregion
        #region Observer
        private List<IStatObserver> m_Observers = new List<IStatObserver>();
        public void AttachObserver(IStatObserver observer) => m_Observers.Add(observer);
        protected void NotifyObservers() => m_Observers.ForEach(observer => observer.OnNotify(Type, Modify));
        #endregion
    }
}



