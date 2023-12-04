using UnityEngine;
namespace EndlessWinter.Stat
{
    [CreateAssetMenu(fileName = "Hunger", menuName = "EndlessWinter/CreateStat/Hunger")]
    public class Hunger : Stat
    {
        #region Property
        internal override float Modify
        {
            get => base.Modify;
            set => base.Modify = value;
        }
        #endregion
    }
}

