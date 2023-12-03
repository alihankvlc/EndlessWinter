using System;
using System.Collections;
using UnityEngine;

public interface IStatObserver
{
    void OnStatChanged(StatType statType, float value);
}
