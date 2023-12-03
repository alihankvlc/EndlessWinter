using System;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "TimeTracker", menuName = "EndlessWinter/CreateTimeTracker")]
public class TimeTracker : ScriptableObject
{
    [Range(1, 6)][SerializeField] private int m_InitialDay;
    [Range(0, 23)][SerializeField] private int m_InitialHours;
    [Range(0, 59)][SerializeField] private int m_InitialMinutes;
    [SerializeField] private float m_TimeMultipler;

    private int m_Days = 1;
    private int m_Hours;
    private int m_Minute;
    private float m_ElapsedSeconds;
    private List<string> m_DaysList;

    public event EventHandler<TimeChangedEventArgs> DayChangedEvent;
    public event EventHandler<TimeChangedEventArgs> HourChangedEvent;
    public event EventHandler<TimeChangedEventArgs> MinuteChangedEvent;
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
    private void OnEnable()
    {
        string[] daysArray = { "Mon", "Tue", "Wed", "Thu", "Fri", "Sat", "Sun" };
        m_DaysList = daysArray.ToList();

        UIManager.Instance.TimeTextMeshPro?.SetText(GetFormattedTime());
    }
#if UNITY_EDITOR
    private void OnValidate()
    {
        if (!Application.isPlaying)
        {
            m_Days = m_InitialDay;
            m_Hours = m_InitialHours;
            m_Minute = m_InitialMinutes;

            UIManager.Instance.TimeTextMeshPro?.SetText(GetFormattedTime());
        }
    }
#endif
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
        if (Hour > 23)
        {
            Hour = 0;
            Day++;
        }
    }
    private void SetAndInvokeIfChanged(ref int field, int param, Action action)
    {
        if (field != param)
        {
            field = param;
            action?.Invoke();
        }
    }
    protected virtual void OnDayChanged() => DayChangedEvent?.Invoke(this, new TimeChangedEventArgs(Day));
    protected virtual void OnHourChanged() => HourChangedEvent?.Invoke(this, new TimeChangedEventArgs(Hour));
    protected virtual void OnMinuteChanged() => MinuteChangedEvent?.Invoke(this, new TimeChangedEventArgs(Minute));
}