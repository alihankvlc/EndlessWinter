using System;
using UnityEngine;
using UnityEngine.Events;

[Serializable]
public class OnTimeChangedEvent : UnityEvent<TimeChangedEventArgs> { }
public class TimeManager : MonoBehaviour
{
    [SerializeField] private TimeTracker m_TimeTracker;
    [Space]
    [SerializeField] private OnTimeChangedEvent m_ChangeDayEvent;
    [SerializeField] private OnTimeChangedEvent m_ChangeHourEvent;
    [SerializeField] private OnTimeChangedEvent m_ChangeMinuteEvent;
    [SerializeField] private OnTimeChangedEvent m_OnDayEvent;
    [SerializeField] private OnTimeChangedEvent m_OnNightEvent;
    private void Start()
    {
        m_TimeTracker.DayChangedEvent += Tracker_DayChangedEvent;
        m_TimeTracker.HourChangedEvent += Tracker_HourChangedEvent;
        m_TimeTracker.MinuteChangedEvent += Tracker_MinuteChangedEvent;
    }
    private void Update()
    {
        m_TimeTracker.UpdateTime();
    }
    #region UnityEventFuncs
    private void Tracker_DayChangedEvent(object sender, TimeChangedEventArgs e)
    => m_ChangeDayEvent?.Invoke(e);
    private void Tracker_HourChangedEvent(object sender, TimeChangedEventArgs e)
        => m_ChangeHourEvent?.Invoke(e);
    private void Tracker_MinuteChangedEvent(object sender, TimeChangedEventArgs e)
        => m_ChangeMinuteEvent?.Invoke(e);
    #endregion
    private void OnDestroy()
    {
        m_TimeTracker.DayChangedEvent -= Tracker_DayChangedEvent;
        m_TimeTracker.HourChangedEvent -= Tracker_HourChangedEvent;
        m_TimeTracker.MinuteChangedEvent -= Tracker_MinuteChangedEvent;
    }
}