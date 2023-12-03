using DG.Tweening;
using Player.Stat;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class UIManager : Singleton<UIManager>
{
    [SerializeField] private List<UIStatUpdater> m_UIStatList = new List<UIStatUpdater>();
    [SerializeField] private TextMeshProUGUI m_TimeTextMesh;
    private Dictionary<StatType, UIStatUpdater> m_UIStatDataDic = new Dictionary<StatType, UIStatUpdater>();

    public TextMeshProUGUI TimeTextMeshPro
    {
        get => m_TimeTextMesh;
        set
        {
            m_TimeTextMesh = value;
        }
    }
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
    }
}
