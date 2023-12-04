using System.Collections;
using UnityEngine;

namespace EndlessWinter.Item
{
    [CreateAssetMenu(fileName = "New_Food_Item", menuName = "EndlessWinter/CreateItem/Consumable/Food")]
    public class Food : Consumable
    {
        [Space]
        [Header("Food Effects")]
        #region Variables
        [SerializeField] internal int HealthRegen;
        [SerializeField] internal int HungerRestoration;
        [SerializeField] internal float CookingTime;
        [SerializeField] internal float BuffDuration;
        [SerializeField] internal bool RequiresCooking;
        [SerializeField] internal bool IsSpoiled;
        [SerializeField] internal bool IsReusable;
        #endregion
    }
}