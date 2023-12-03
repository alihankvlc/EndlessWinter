using System;
using UnityEngine;

public class TimeTracker
{
    private int m_Day;
    private int m_Hour;
    private int m_Minute;

    public event EventHandler<TimeChangedEventArgs> DayChangedEvent;
    public event EventHandler<TimeChangedEventArgs> HourChangedEvent;
    public event EventHandler<TimeChangedEventArgs> MinuteChangedEvent;

    public int Day
    {
        get => m_Day;
        set
        {
            m_Day = m_Day != value ? value : m_Day;
            OnDayChanged();
        }
    }
    public int Hour
    {
        get => m_Hour;
        set
        {
            m_Hour = m_Hour != value ? value : m_Hour;
            OnHourChanged();
        }
    }
    public int Minute
    {
        get => m_Minute;
        set
        {
            m_Minute = m_Minute != value ? value : m_Minute;
            OnMinuteChanged();
        }
    }
    protected virtual void OnDayChanged() => DayChangedEvent?.Invoke(this, new TimeChangedEventArgs(Day));
    protected virtual void OnHourChanged() => HourChangedEvent?.Invoke(this, new TimeChangedEventArgs(Hour));
    protected virtual void OnMinuteChanged() => MinuteChangedEvent?.Invoke(this, new TimeChangedEventArgs(Minute));
}