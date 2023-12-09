namespace EndlessWinter.Stat
{
    using UnityEngine;

    [CreateAssetMenu(fileName = "Stamina", menuName = "EndlessWinter/CreateStat/Stamina")]
    public class Stamina : Stat
    {
        #region StaminaVariables
        [Space]
        [Header("Stamina Settings")]
        [SerializeField] private float m_IncreaseAmount;
        [SerializeField] private float m_DecreaseAmount;
        #endregion
        #region Property
        public float IncreaseAmount => m_IncreaseAmount;
        public float DecreaseAmount => m_IncreaseAmount;
        internal override float Modify
        {
            get => base.Modify;
            set => base.Modify = value;
        }
        #endregion
    }
}
