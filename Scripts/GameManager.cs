using UnityEngine;
using EndlessWinter.Weather;
using EndlessWinter.Stat;

public class GameManager : MonoBehaviour
{
    #region Variables
    [SerializeField] private Weather m_Weather;
    [SerializeField] private float m_Temperature;
    [SerializeField] private WeatherType m_ActiveWeatherType; 
    #endregion
    private void Start()
    {
        m_Weather.InitializeWeather();
    }
}
