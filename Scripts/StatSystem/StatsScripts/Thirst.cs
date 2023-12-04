using UnityEngine;
namespace EndlessWinter.Stat
{
    [CreateAssetMenu(fileName = "Thirst", menuName = "EndlessWinter/CreateStat/Thirst")]
    public class Thirst : Stat
    {
        #region Variables
        internal override float Modify
        {
            get => base.Modify;
            set => base.Modify = value;
        } 
        #endregion
    }
}