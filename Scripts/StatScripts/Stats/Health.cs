using UnityEngine;
namespace Player.Stat
{
    [CreateAssetMenu(fileName = "Health", menuName = "EndlessWinter/CreateStat/Health")]
    public class Health : Stat
    {
        [field: SerializeField]
        public bool IsAlive { get; private set; }
        internal override float Modify
        {
            get => base.Modify;
            set
            {
                base.Modify = value;
                IsAlive = CurrentValue > 0;
            }
           
        }

#if UNITY_EDITOR
        private void OnValidate()
        {
            if (!Application.isPlaying)
            {
                IsAlive = CurrentValue > 0;
            }
        }
#endif
    }
}
