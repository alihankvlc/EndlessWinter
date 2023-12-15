namespace EndlessWinter.Weather
{
    using System;
    using UnityEngine;
    using EndlessWinter.Weather;
    public class NotifyChangedEventArgs : EventArgs
    {
        public int Time;
        public Weather Weather;
        public NotifyChangedEventArgs(int Time) => this.Time = Time;
        public NotifyChangedEventArgs(Weather weather) => this.Weather = weather;

        //Düzenlenecek.
    }
}

