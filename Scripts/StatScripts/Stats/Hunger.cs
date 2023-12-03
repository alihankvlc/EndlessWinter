using UnityEngine;
namespace Player.Stat
{
    [CreateAssetMenu(fileName = "Hunger", menuName = "EndlessWinter/CreateStat/Hunger")]
    public class Hunger : Stat
    {
        internal override float Modify { get => base.Modify; set => base.Modify = value; }
    }
}

