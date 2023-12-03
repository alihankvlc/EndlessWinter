using UnityEngine;
namespace Player.Stat
{
    [CreateAssetMenu(fileName = "Temperature", menuName = "EndlessWinter/CreateStat/Temperature")]
    public class Temperature : Stat
    {
        internal override float Modify { get => base.Modify; set => base.Modify = value; }
    }
}