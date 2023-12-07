using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "TimeTracker", menuName = "EndlessWinter/CreateTimeTracker")]
public class TimeTracker : ScriptableObject
{
    #region Variables
    [Range(1, 6), SerializeField] private int m_InitialDay;
    [Range(0, 23), SerializeField] private int m_InitialHours;
    [Range(0, 59), SerializeField] private int m_InitialMinutes;
    [Range(0, 23), SerializeField] private int m_SunriseTime;
    [Range(0, 23), SerializeField] private int m_SunsetTime;
    [SerializeField] private float m_TimeMultipler;

    private int m_Days = 1;
    private int m_Hours;
    private int m_Minute;
    private float m_ElapsedSeconds;
    private List<string> m_DaysList;

    public event EventHandler<StateChangedEventArgs> DayChangedEvent;
    public event EventHandler<StateChangedEventArgs> HourChangedEvent;
    public event EventHandler<StateChangedEventArgs> MinuteChangedEvent;
    #endregion
    #region Property
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
    #region Funcs
    private void OnEnable()
    {
        string[] daysArray = { "Mon", "Tue", "Wed", "Thu", "Fri", "Sat", "Sun" };
        m_DaysList = daysArray.ToList();

        UIManager.Instance.TimeTextMeshPro?.SetText(GetFormattedTime());
    }
    private void OnValidate()
    {
        if (!Application.isPlaying)
        {
            m_Days = m_InitialDay = Day;
            m_Hours = m_InitialHours = Hour;
            m_Minute = m_InitialMinutes = Minute;

            UIManager.Instance.TimeTextMeshPro?.SetText(GetFormattedTime());
        }
    }
    public string GetFormattedTime() =>
    $"DAY:{Day} {Hour:D2}:{Minute:D2}\n{m_DaysList[(m_Days + 6) % m_DaysList.Count]}";
    public void UpdateTime()
    {
        m_ElapsedSeconds += Time.deltaTime * m_TimeMultipler;
        while (m_ElapsedSeconds > 59)
        {
            IncrementMinute();
            m_ElapsedSeconds -= 60;
            UIManager.Instance.TimeTextMeshPro?.SetText(GetFormattedTime());
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
        m_InitialHours++;
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

    protected virtual void OnDayChanged() => DayChangedEvent?.Invoke(this, new StateChangedEventArgs(Day));
    protected virtual void OnHourChanged() => HourChangedEvent?.Invoke(this, new StateChangedEventArgs(Hour));
    protected virtual void OnMinuteChanged() => MinuteChangedEvent?.Invoke(this, new StateChangedEventArgs(Minute));
    #endregion
}