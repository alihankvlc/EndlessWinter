using System.Collections;
using UnityEngine;

namespace EndlessWinter.Item
{
    [CreateAssetMenu(fileName = "New_Clothes_Item", menuName = "EndlessWinter/CreateItem/Clothes")]
    public class Clothes : Item
    {
        #region Variables
        [Space]
        [Header("Clothes Variables")]
        [SerializeField] internal ClothesType ClothesType;
        [SerializeField] internal float InsulationValue;
        #endregion
    }
}