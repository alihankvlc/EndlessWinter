using System;
using UnityEngine.Events;
using UnityEngine;
using EndlessWinter.Weather;
using System.Collections.Generic;
using System.Linq;
using EndlessWinter.Stat;
using System.Collections;

[Serializable]
public class OnTimeChangedEvent : UnityEvent<StateChangedEventArgs> { }
public class GameManager : Singleton<GameManager>
{
    #region TimeVariables
    [Header("Time Settings")]
    [SerializeField] private TimeTracker m_TimeTracker;
    #endregion
    #region WeatherVariables
    [Header("Weather Settings")]
    [SerializeField] private WeatherType m_CurrentWeather;
    [SerializeField] private WeatherType m_NextWeather;
    [SerializeField] private List<Weather> m_WeatherList = new List<Weather>();
    private Dictionary<WeatherType, Weather> m_WeatherDictionary;

    private bool m_CanDetermineWeather;
    private int m_RandomMinute;
    private int m_RandomHour;

    private EventHandler<StateChangedEventArgs> StateChangedEvent;
    #endregion
    #region OnTimeChangedEventVariables
    [SerializeField] private OnTimeChangedEvent m_ChangeDayEvent;
    [SerializeField] private OnTimeChangedEvent m_ChangeHourEvent;
    [SerializeField] private OnTimeChangedEvent m_ChangeMinuteEvent;
    [SerializeField] private OnTimeChangedEvent m_ChangeWeatherEvent;
    #endregion

    [SerializeField] private float m_GlobalTemperature;

    private float m_ChangeTempValue;

    private bool m_CanChangeTemp;
    private void Start()
    {
        #region SubscribeEvent
        m_TimeTracker.DayChangedEvent += Tracker_DayChangedEvent;
        m_TimeTracker.HourChangedEvent += Tracker_HourChangedEvent;
        m_TimeTracker.MinuteChangedEvent += Tracker_MinuteChangedEvent;
        StateChangedEvent += WeatherChangerdEvent;
        #endregion

        if (m_WeatherDictionary == null) m_WeatherDictionary = new Dictionary<WeatherType, Weather>();

        m_WeatherDictionary = m_WeatherList
            .Where(weather => weather.Rarity != RarityType.None && weather.Type != WeatherType.None)
            .ToDictionary(weather => weather.Type);

        DetermineNextWeather();
        m_CurrentWeather = WeatherType.Sunny;


        m_ChangeMinuteEvent.AddListener(UpdateWeather);
        m_ChangeWeatherEvent.AddListener(GetWeatherNotify);
    }
    #region UnSubscribe
    private void OnDestroy()
    {
        m_TimeTracker.DayChangedEvent -= Tracker_DayChangedEvent;
        m_TimeTracker.HourChangedEvent -= Tracker_HourChangedEvent;
        m_TimeTracker.MinuteChangedEvent -= Tracker_MinuteChangedEvent;
        StateChangedEvent -= WeatherChangerdEvent;
    }
    #endregion
    private void Update()
    {
        m_TimeTracker.UpdateTime();

        if (m_CanChangeTemp)
            UpdateTemp();
    }
    #region WeatherFuncs
    private void UpdateWeather(StateChangedEventArgs e)
    {
        if (m_CanDetermineWeather)
            DetermineNextWeather();

        if (m_WeatherDictionary.TryGetValue(m_CurrentWeather, out Weather wSettings))
        {
            if (m_RandomMinute == m_TimeTracker.Minute && m_RandomHour == m_TimeTracker.Hour)
            {
                m_CurrentWeather = m_NextWeather;
                m_CanDetermineWeather = true;
            }
        }
    }
    private RarityType DetermineRarity(float randomValue)
    {
        foreach (RarityType rarity in Enum.GetValues(typeof(RarityType)))
        {
            if (randomValue <= (float)rarity / 100)
                return rarity;
        }

        return RarityType.None;


        /*çok düşük ihtimal olsa bile legendary hava olayları gelebiliyor.. 
         * 
         * Belirli
         bir gün eşiğinde Legendary,Epic tipine sahip hava olayları yaşanacak.*/
    }
    private void DetermineNextWeather()
    {
        m_RandomMinute = UnityEngine.Random.Range(0, 59);
        m_RandomHour = UnityEngine.Random.Range(0, 23);

        float randomValue = UnityEngine.Random.Range(0f, 1f);
        RarityType rarity = DetermineRarity(randomValue);
        m_NextWeather = m_WeatherList.FirstOrDefault(r => r.Rarity == rarity)?.Type ?? WeatherType.Sunny;
        m_CanDetermineWeather = false;

        if (m_WeatherDictionary.TryGetValue(m_CurrentWeather, out Weather wSettings))
            StateChangedEvent?.Invoke(this, new StateChangedEventArgs(wSettings));
    }
    #endregion
    #region m_Funcs
    private void GetWeatherNotify(StateChangedEventArgs e)
    {
        if (m_CanChangeTemp) return;

        float weatherTemp = e.Weather.TemperatureEffect();
        m_ChangeTempValue = weatherTemp;
        m_CanChangeTemp = true;
    }
    #endregion
    private void UpdateTemp()
    {
        if (!m_CanChangeTemp) return;

        m_GlobalTemperature = m_GlobalTemperature = Mathf.MoveTowards(
            m_GlobalTemperature, m_ChangeTempValue, Time.deltaTime
            );

        string tempText = $"{m_GlobalTemperature.ToString("F0")}°C";
        UIManager.Instance.TempTextMeshPro.SetText(tempText);

        if (m_GlobalTemperature == m_ChangeTempValue)
        {
            m_ChangeTempValue = 0.0f;
            m_CanChangeTemp = false;
        }

    }
    #region InitializeUnityEventFuncs
    public bool IsSpecificTime(int Hour, int Minute) => m_TimeTracker.Minute == Minute && m_TimeTracker.Hour == Hour;
    private void Tracker_DayChangedEvent(object sender, StateChangedEventArgs e)
    => m_ChangeDayEvent?.Invoke(e);
    private void Tracker_HourChangedEvent(object sender, StateChangedEventArgs e)
        => m_ChangeHourEvent?.Invoke(e);
    public void WeatherChangerdEvent(object sender, StateChangedEventArgs e)
        => m_ChangeWeatherEvent.Invoke(e);
    private void Tracker_MinuteChangedEvent(object sender, StateChangedEventArgs e)
        => m_ChangeMinuteEvent?.Invoke(e);
    #endregion
}