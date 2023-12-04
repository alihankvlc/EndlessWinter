using UnityEngine;
namespace EndlessWinter.Stat
{
    [CreateAssetMenu(fileName = "Fatigue", menuName = "EndlessWinter/CreateStat/Fatigue")]
    public class Fatigue : Stat
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

