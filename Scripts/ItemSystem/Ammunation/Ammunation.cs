using System.Collections;
using UnityEngine;

namespace EndlessWinter.Item
{
    [CreateAssetMenu(fileName = "New_Ammo_Item", menuName = "EndlessWinter/CreateItem/Weapon/Ammo")]
    public class Ammunation : Item
    {
        #region Variables
        [Header("Ammo Variables")]
        [SerializeField] internal WeaponType WeaponType; 
        #endregion
    }
}