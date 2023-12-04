using System.Collections;
using UnityEngine;

namespace EndlessWinter.Item
{
    [CreateAssetMenu(fileName = "New_Firearms_Item", menuName = "EndlessWinter/CreateItem/Weapon/Firearms")]
    public class Firearms : Item
    {
        #region Variables
        [Space]
        [Header("Weapon Variables")]
        [SerializeField] internal WeaponType WeaponType;
        [SerializeField] internal Ammunation Ammo;
        [SerializeField] internal int AmmoCapacity;
        [SerializeField] internal int AttackDamage;
        [SerializeField] internal float AttackSpeed;
        [SerializeField] internal float ReloadTime;
        #endregion
    }
}