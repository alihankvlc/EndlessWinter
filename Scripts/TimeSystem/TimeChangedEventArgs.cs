using System;
using UnityEngine;

public class TimeChangedEventArgs : EventArgs
{
    public int Time;
    public TimeChangedEventArgs(int Time) => this.Time = Time;
}
