using System.Collections.Generic;
using UnityEngine;
public class Stat
{
    public StatType Type { get; private set; }
    public float MaxValue { get; private set; }
    private float _currentValue;

    private List<IStatObserver> _observers = new List<IStatObserver>();

    public Stat(StatType type, float maxValue)
    {
        Type = type;
        MaxValue = maxValue;
        _currentValue = maxValue;
    }
    public float CurrentValue
    {
        get { return _currentValue; }
        set
        {
            _currentValue = Mathf.Clamp(value, 0f, MaxValue);
            NotifyObservers();
        }
    }
    public void AddObserver(IStatObserver observer)
    {
        _observers.Add(observer);
    }
    public void RemoveObserver(IStatObserver observer)
    {
        _observers.Remove(observer);
    }
    private void NotifyObservers()
    {
        foreach (var observer in _observers)
        {
            observer.OnStatChanged(Type, _currentValue);
        }
    }
}
