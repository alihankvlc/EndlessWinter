using UnityEngine;
namespace Player.Stat
{
    [CreateAssetMenu(fileName = "Thirst", menuName = "EndlessWinter/CreateStat/Thirst")]
    public class Thirst : Stat
    {
        internal override float Modify { get => base.Modify; set => base.Modify = value; }
    }
}