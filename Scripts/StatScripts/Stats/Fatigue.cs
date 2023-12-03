using UnityEngine;
namespace Player.Stat
{
    [CreateAssetMenu(fileName = "Fatigue", menuName = "EndlessWinter/CreateStat/Fatigue")]
    public class Fatigue : Stat
    {
        internal override float Modify { get => base.Modify; set => base.Modify = value; }
    }
}

