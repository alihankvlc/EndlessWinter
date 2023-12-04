using System.Collections;
using UnityEngine;

namespace EndlessWinter.Item
{
    [CreateAssetMenu(fileName = "New_Melee_Item", menuName = "EndlessWinter/CreateItem/Weapon/Melee")]
    public class Melee : Item
    {
        #region Variables
        [Header("Melee Variables")]
        [SerializeField] internal WeaponType WeaponType;
        [SerializeField] internal float Range;
        [SerializeField] internal bool IsTwoHanded;
        [SerializeField] internal bool HasBlade;
        [SerializeField] internal bool HasBluntForce; 
        #endregion
    }
}