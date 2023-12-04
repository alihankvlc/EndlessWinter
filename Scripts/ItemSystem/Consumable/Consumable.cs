using System.Collections;
using UnityEngine;

namespace EndlessWinter.Item
{
    public abstract class Consumable : Item
    {
        #region Variables
        [Space]
        [Header("Consumable Variables")]
        [SerializeField] internal ConsumableType ConsumableType;
        [SerializeField] internal bool GrantsBuff;           // Tüketildiğinde geçici avantaj, dezavantaj, vs.
        [SerializeField] internal float CooldownDuration;    // Tekrar kullanılabilirse tekrar kullanılabilmesi için geçmesi gereken süre
        [SerializeField] internal int FatigueReduction;      // Tüketildiğinde yorgunluk azalışı...
        #endregion
    }
}