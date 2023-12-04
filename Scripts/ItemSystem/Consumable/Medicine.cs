using System.Collections;
using UnityEngine;

namespace EndlessWinter.Item
{
    [CreateAssetMenu(fileName = "New_Medicine_Item", menuName = "EndlessWinter/CreateItem/Consumable/Medicine")]
    public class Medicine : Consumable
    {
        #region Variables
        [SerializeField] internal int HealthRegenBoost;
        [SerializeField] internal int Duration;
        #endregion

        //Düzenlenecek
    }
}