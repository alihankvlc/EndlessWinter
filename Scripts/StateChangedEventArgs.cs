namespace EndlessWinter.Weather
{
    using System;
    using UnityEngine;
    using EndlessWinter.Weather;
    public class StateChangedEventArgs : EventArgs
    {
        public int Time;
        public Weather Weather;
        public StateChangedEventArgs(int Time) => this.Time = Time;
        public StateChangedEventArgs(Weather weather) => this.Weather = weather;
    }
}

