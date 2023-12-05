﻿using System;
using UnityEngine.Events;
using UnityEngine;

[Serializable]
public class OnTimeChangedEvent : UnityEvent<TimeChangedEventArgs> { }
public class TimeManager : Singleton<TimeManager>
{
    #region Variables
    [SerializeField] private TimeTracker m_TimeTracker; 
    #endregion
    [Space]
    #region OnTimeChangedEventVariables
    [SerializeField] private OnTimeChangedEvent m_ChangeDayEvent;
    [SerializeField] private OnTimeChangedEvent m_ChangeHourEvent;
    [SerializeField] private OnTimeChangedEvent m_ChangeMinuteEvent;
    [SerializeField] private OnTimeChangedEvent m_OnDayEvent;
    [SerializeField] private OnTimeChangedEvent m_OnNightEvent;
    #endregion
    #region Property
    public int GetMinute => m_TimeTracker.Minute;
    public int GetHour => m_TimeTracker.Hour;
    #endregion

    private void Start()
    {
        #region Subscribe
        m_TimeTracker.DayChangedEvent += Tracker_DayChangedEvent;
        m_TimeTracker.HourChangedEvent += Tracker_HourChangedEvent;
        m_TimeTracker.MinuteChangedEvent += Tracker_MinuteChangedEvent;
        #endregion
    }
    private void OnDestroy()
    {
        #region Unsubscribe
        m_TimeTracker.DayChangedEvent -= Tracker_DayChangedEvent;
        m_TimeTracker.HourChangedEvent -= Tracker_HourChangedEvent;
        m_TimeTracker.MinuteChangedEvent -= Tracker_MinuteChangedEvent;
        #endregion
    }
    private void Update() => m_TimeTracker.UpdateTime();
    #region Funcs
    public bool IsSpecificTime(int Hour, int Minute) => m_TimeTracker.Minute == Minute && m_TimeTracker.Hour == Hour;
    private void Tracker_DayChangedEvent(object sender, TimeChangedEventArgs e)
    => m_ChangeDayEvent?.Invoke(e);
    private void Tracker_HourChangedEvent(object sender, TimeChangedEventArgs e)
        => m_ChangeHourEvent?.Invoke(e);
    private void Tracker_MinuteChangedEvent(object sender, TimeChangedEventArgs e)
        => m_ChangeMinuteEvent?.Invoke(e);
    #endregion
}