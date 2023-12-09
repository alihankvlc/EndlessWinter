using System.Collections.Generic;
using System.Linq;
using EndlessWinter.Manager;
using System;
using UnityEngine;
using EndlessWinter.Weather;

[CreateAssetMenu(fileName = "TimeTracker", menuName = "EndlessWinter/CreateTimeTracker")]
public class TimeTracker : ScriptableObject
{
    #region Variables
    [Range(0, 23)][SerializeField] private int m_SunriseHour = 6;
    [Range(0, 23)][SerializeField] private int m_SunsetHour = 18;
    [SerializeField] private float m_TimeMultiplier = 1f;

    private int m_Days = 1;
    private int m_Hours;
    private int m_Minute;
    private bool m_IsNight;

    private float m_ElapsedSeconds;
    private List<string> m_DaysList;

    public event EventHandler<StateChangedEventArgs> DayChangedEvent;
    public event EventHandler<StateChangedEventArgs> HourChangedEvent;
    public event EventHandler<StateChangedEventArgs> MinuteChangedEvent;

    public event Action SunriseEvent;
    public event Action SunsetEvent;

    #endregion

    #region Properties
    public bool IsNight => m_IsNight;
    public int Day
    {
        get => m_Days;
        private set => SetAndInvokeIfChanged(ref m_Days, value, OnDayChanged);
    }
    public int Hour
    {
        get => m_Hours;
        private set => SetAndInvokeIfChanged(ref m_Hours, value, OnHourChanged);
    }
    public int Minute
    {
        get => m_Minute;
        private set => SetAndInvokeIfChanged(ref m_Minute, value, OnMinuteChanged);
    }
    #endregion

    #region Functions
    public void InitializeTimeTracker()
    {
        string[] daysArray = { "Mon", "Tue", "Wed", "Thu", "Fri", "Sat", "Sun" };
        m_DaysList = daysArray.ToList();
        m_Days = 1;
        UIManager.Instance.TMP_Time?.SetText(GetFormattedTime());
    }
    public string GetFormattedTime() =>
        $"DAY:{Day} {Hour:D2}:{Minute:D2}\n{m_DaysList[(m_Days + 6) % m_DaysList.Count]}";

    public void UpdateTime()
    {
        m_ElapsedSeconds += Time.deltaTime * m_TimeMultiplier;
        while (m_ElapsedSeconds > 59)
        {
            IncrementMinute();
            UpdateDayNightStatus();
            m_ElapsedSeconds -= 60;
            UIManager.Instance.TMP_Time?.SetText(GetFormattedTime());
        }
    }

    private void UpdateDayNightStatus()
    {
        bool isNight = Hour < m_SunriseHour || Hour >= m_SunsetHour;

        if (isNight != m_IsNight)
        {
            if (isNight)
                SunsetEvent.Invoke();
            else
                SunriseEvent?.Invoke();

            m_IsNight = isNight;
        }
    }
    private void IncrementMinute()
    {
        Minute++;
        if (Minute > 59)
        {
            Minute = 0;
            IncrementHour();
        }
    }
    private void IncrementHour()
    {
        Hour++;

        if (Hour > 23)
        {
            Hour = 0;
            Day++;
        }
    }
    private void SetAndInvokeIfChanged(ref int field, int value, Action action)
    {
        if (field != value)
        {
            field = value;
            action?.Invoke();
        }
    }
    protected virtual void OnDayChanged() =>
        DayChangedEvent?.Invoke(this, new StateChangedEventArgs(Day));
    protected virtual void OnHourChanged() =>
        HourChangedEvent?.Invoke(this, new StateChangedEventArgs(Hour));
    protected virtual void OnMinuteChanged() =>
        MinuteChangedEvent?.Invoke(this, new StateChangedEventArgs(Minute));
    #endregion
}

