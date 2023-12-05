using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace EndlessWinter.Weather
{
    [CreateAssetMenu(fileName = "New_Weather", menuName = "EndlessWinter/CreateWeather")]
    public class Weather : ScriptableObject
    {
        #region Variables
        [Header("General Settings")]
        public WeatherType CurrentWeather;
        public WeatherType NextWeather;
        public List<WeatherSettings> m_WeatherSettings = new List<WeatherSettings>();
        private Dictionary<WeatherType, WeatherSettings> m_WeatherSettingCache;

        private bool m_CanDetermineWeather;
        private int m_RandomMinute;
        private int m_RandomHour;
        #endregion
        #region Funcs
        public void InitializeWeather()
        {
            InitializeWindsCache();
            DetermineNextWeather();
        }
        private void InitializeWindsCache()
        {
            if (m_WeatherSettingCache == null)
                m_WeatherSettingCache = new Dictionary<WeatherType, WeatherSettings>();

            m_WeatherSettingCache = m_WeatherSettings.ToDictionary(weatherSettings => weatherSettings.WeatherType);
        }

        public void UpdateWeather()
        {
            int Minute = TimeManager.Instance.GetMinute;
            int Hour = TimeManager.Instance.GetHour;

            if (m_CanDetermineWeather)
                DetermineNextWeather();

            if (m_WeatherSettingCache.TryGetValue(CurrentWeather, out WeatherSettings wSettings))
            {
                if (m_RandomMinute == Minute && m_RandomHour == Hour)
                {
                    CurrentWeather = NextWeather;
                    m_CanDetermineWeather = true;
                }
            }

        }
        public WeatherSettings GetWeatherSettings()
        {
            if (m_WeatherSettingCache.TryGetValue(CurrentWeather, out WeatherSettings wSettings))
            {
                return wSettings;
            }
            return null;
        }

        private RarityType DetermineRarity(float randomValue)
        {
            foreach (RarityType rarity in Enum.GetValues(typeof(RarityType)))
            {
                if (randomValue <= (float)rarity / 100)
                    return rarity;
            }

            return RarityType.None;
        }
        private void DetermineNextWeather()
        {
            GenerateRandomTime();
            float randomValue = UnityEngine.Random.Range(0f, 1f);
            RarityType rarity = DetermineRarity(randomValue);
            NextWeather = m_WeatherSettings.FirstOrDefault(r => r.RarityType == rarity)?.WeatherType ?? WeatherType.Default;
            m_CanDetermineWeather = false;
        }
        private void GenerateRandomTime()
        {
            m_RandomMinute = UnityEngine.Random.Range(0, 59);
            m_RandomHour = UnityEngine.Random.Range(0, 23);
            Debug.Log($"Random Time:{m_RandomHour:00}:{m_RandomMinute:00}");
        } 
        #endregion
    }

}
