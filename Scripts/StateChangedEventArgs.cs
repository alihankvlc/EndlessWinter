using EndlessWinter.Weather;
using System;
using UnityEngine;

public class StateChangedEventArgs : EventArgs
{
    public int Time;
    public Weather Weather;
    public bool IsNight;
    public StateChangedEventArgs(int Time) => this.Time = Time;
    public StateChangedEventArgs(Weather weather) => this.Weather = weather;
    public StateChangedEventArgs(bool isNight) => this.IsNight = isNight;
}
