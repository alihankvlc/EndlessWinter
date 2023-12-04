using System.Collections;
using UnityEngine;

namespace EndlessWinter.Item
{
    [CreateAssetMenu(fileName = "New_Drink_Item", menuName = "EndlessWinter/CreateItem/Consumable/Drink")]
    public class Drink : Consumable
    {
        [Space]
        [Header("Drink Variables")]
        #region Variables
        [SerializeField] internal int ThirstRestoration;
        [SerializeField] internal int EnergyBoost;
        [SerializeField] internal bool IsHot;
        [SerializeField] internal bool IsEnergizing;
        #endregion
    }
}