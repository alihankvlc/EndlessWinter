using System;
using UnityEngine;
using UnityEngine.Events;

public class TimeController : MonoBehaviour
{
    [Serializable]
    public class OnTimeChangedEvent : UnityEvent<TimeChangedEventArgs> { }

    [Space]
    [SerializeField] private OnTimeChangedEvent m_ChangeDayEvent;
    [SerializeField] private OnTimeChangedEvent m_ChangeHourEvent;
    [SerializeField] private OnTimeChangedEvent m_ChangeMinuteEvent;

    private TimeTracker m_TimeTracker;
    private void Start()
    {
        m_TimeTracker.DayChangedEvent += Tracker_DayChangedEvent;
        m_TimeTracker.HourChangedEvent += Tracker_HourChangedEvent;
        m_TimeTracker.MinuteChangedEvent += Tracker_MinuteChangedEvent;
    }
    private void Tracker_DayChangedEvent(object sender, TimeChangedEventArgs e)
        => m_ChangeDayEvent?.Invoke(e);
    private void Tracker_HourChangedEvent(object sender, TimeChangedEventArgs e)
        => m_ChangeHourEvent?.Invoke(e);
    private void Tracker_MinuteChangedEvent(object sender, TimeChangedEventArgs e)
        => m_ChangeDayEvent?.Invoke(e);
}