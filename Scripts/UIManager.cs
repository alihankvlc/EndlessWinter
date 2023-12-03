<<<<<<< HEAD
﻿using DG.Tweening;
using Player.Stat;
=======
﻿using System;
using System.Collections;
>>>>>>> parent of 3211f14 (Update)
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class UIManager : Singleton<UIManager>
{
<<<<<<< HEAD
    [SerializeField] private List<UIStatUpdater> m_UIStatList = new List<UIStatUpdater>();
    [SerializeField] private TextMeshProUGUI m_TimeTextMesh;
    private Dictionary<StatType, UIStatUpdater> m_UIStatDataDic = new Dictionary<StatType, UIStatUpdater>();

    protected override void Awake()
    {
        DOTween.SetTweensCapacity(5000, 500);
    }
    private void Start()
    {
        m_UIStatDataDic = m_UIStatList.ToDictionary(uiUpdater => uiUpdater.Stat.Type);
    }
    public UIStatUpdater UIStatUpdater(StatType type)
    {
        if (m_UIStatDataDic.TryGetValue(type, out UIStatUpdater stat))
        {
            return stat;
        }
        return null;
=======
    #region TextMeshPro
    [SerializeField] private TextMeshProUGUI m_HealthText;
    [SerializeField] private TextMeshProUGUI m_StaminaText;
    [SerializeField] private TextMeshProUGUI m_ThirstText;
    [SerializeField] private TextMeshProUGUI m_ProteinText;
    [SerializeField] private TextMeshProUGUI m_FatText;
    [SerializeField] private TextMeshProUGUI m_CarbText;
    #endregion
    private Dictionary<StatType, TextMeshProUGUI> m_StatTextMeshDic = new Dictionary<StatType, TextMeshProUGUI>();

    private void Start()
    {
        InitializeStatDictionary();
>>>>>>> parent of 3211f14 (Update)
    }
    private void InitializeStatDictionary()
    {
        m_StatTextMeshDic.Add(StatType.Health, m_HealthText);
        m_StatTextMeshDic.Add(StatType.Stamina, m_StaminaText);
        m_StatTextMeshDic.Add(StatType.Thirst, m_ThirstText);
        m_StatTextMeshDic.Add(StatType.Protein, m_ProteinText);
        m_StatTextMeshDic.Add(StatType.Fat, m_FatText);
        m_StatTextMeshDic.Add(StatType.Carb, m_CarbText);
    }
    public void ModifyStatTextMeshPro(StatType statType,float value)
    {
        if (m_StatTextMeshDic.TryGetValue(statType,out TextMeshProUGUI textMesh))
        {
            string statTypeName = Enum.GetName(typeof(StatType), statType);
            textMesh.SetText($"{statTypeName}: {value}");
        }
    }

}
