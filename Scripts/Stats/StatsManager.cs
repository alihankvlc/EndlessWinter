using System;
using System.Collections.Generic;
using UnityEngine;

public class StatsManager : IStatObserver
{
    private Dictionary<StatType, Stat> _stats = new Dictionary<StatType, Stat>();
    private const float INITIALIZE_MAXVALUE_STATS = 100.0f;
    public StatsManager()
    {
        InitializeStats();
    }
    private void InitializeStats()
    {
        foreach (StatType statType in Enum.GetValues(typeof(StatType)))
        {
            _stats[statType] = new Stat(statType, INITIALIZE_MAXVALUE_STATS);
            _stats[statType].AddObserver(this);
            UIManager.Instance.ModifyStatTextMeshPro(statType, INITIALIZE_MAXVALUE_STATS);
        }
    }
    public void OnStatChanged(StatType statType, float newValue)
    {
        UIManager.Instance.ModifyStatTextMeshPro(statType, newValue);
    }
    public void ModifyStat(StatType statType, float amount)
    {
        if (_stats.TryGetValue(statType,out Stat stat))
        {
            stat.CurrentValue += amount;
        }
    }
}
