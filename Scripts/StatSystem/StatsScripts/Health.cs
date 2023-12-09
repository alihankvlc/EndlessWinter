namespace EndlessWinter.Stat
{
    using UnityEngine;

    [CreateAssetMenu(fileName = "Health", menuName = "EndlessWinter/CreateStat/Health")]
    public class Health : Stat
    {
        #region HealthVariables
        [Space]
        [Header("Health Settings")]
        [SerializeField] private bool m_IsAlive;
        #endregion
        #region Property
        public bool IsAlive => m_IsAlive;
        internal override float Modify
        {
            get => base.Modify;
            set
            {
                base.Modify = value;
                m_IsAlive = CurrentValue > 0;
            }

        }
        #endregion

#if UNITY_EDITOR
        private void OnValidate()
        {
            if (!Application.isPlaying)
                m_IsAlive = CurrentValue > 0;
        }
#endif
    }
}
