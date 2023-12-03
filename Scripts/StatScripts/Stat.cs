using System.Collections.Generic;
using UnityEngine;

namespace Player.Stat
{
    public abstract class Stat : ScriptableObject
    {
        [Header("General Settings")]
        [SerializeField] internal StatType Type = StatType.None;
        [SerializeField] internal float CurrentValue = 100;
        [Range(5, 500)]
        [SerializeField] internal int MaxValue = 100;
        [SerializeField] internal Color CustomColor = Color.white;
        [SerializeField] internal Color ThresholdColor = Color.red;
        [Multiline]
        [SerializeField] internal string Inform = "Null";

        [Header("Increase & Decrease Arrow Settings")]
        [SerializeField] internal float FadeInDuration = 0.35f;
        [SerializeField] internal float FadeOutDuration = 0.35f;
        [SerializeField] internal float Delay = 0.0f;

        internal virtual float Modify
        {
            get => CurrentValue;
            set
            {
                CurrentValue = Mathf.Clamp(value, 0, MaxValue);
                NotifyObservers();
            }
        }
        private List<IStatObserver> m_Observers = new List<IStatObserver>();
        public void AttachObserver(IStatObserver observer) => m_Observers.Add(observer);
        protected void NotifyObservers() => m_Observers.ForEach(observer => observer.OnNotify(Type,Modify));
    }
}



