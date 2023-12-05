using System.Collections.Generic;
using UnityEngine;
namespace EndlessWinter.Weather
{
    [System.Serializable]
    public class WeatherSettings
    {
        public WeatherType WeatherType;
        public RarityType RarityType;
        public float WindSpeed;
        public float TemperatureEffects;
        public List<AudioClip> AmbientSounds = new List<AudioClip>();
    }
}