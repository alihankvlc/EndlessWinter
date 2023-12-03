using UnityEngine;
namespace Player.Stat
{
    [CreateAssetMenu(fileName = "Stamina", menuName = "EndlessWinter/CreateStat/Stamina")]
    public class Stamina : Stat
    {
        [field: SerializeField]
        public float IncreaseAmount { get; private set; }
        [field: SerializeField]
        public float DecreaseAmount { get; private set; }
        internal override float Modify { get => base.Modify; set => base.Modify = value; }
    }
}
