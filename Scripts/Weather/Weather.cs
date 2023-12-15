namespace EndlessWinter.Weather
{
    using System.Collections.Generic;
    using UnityEngine;
    using EndlessWinter.UI;
    using EndlessWinter;
    [CreateAssetMenu(fileName = "New_Weather", menuName = "EndlessWinter/CreateWeather")]
    public class Weather : ScriptableObject
    {
        #region Variables
        [Header("General Settings")]
        public WeatherType Type;
        public RarityType Rarity;
        [Min(1)] public float WindSpeed;
        public float MinTemp;
        public float MaxTemp;
        public ParticleSystem VFX;
        public List<AudioClip> AmbientSounds = new List<AudioClip>();
        #endregion
        public float TemperatureEffect()
            => UnityEngine.Random.Range(MinTemp, MaxTemp) * WindSpeed;
    }

}
