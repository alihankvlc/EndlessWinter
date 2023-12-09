namespace EndlessWinter.Stat
{
    using UnityEngine;

    [CreateAssetMenu(fileName = "Fatigue", menuName = "EndlessWinter/CreateStat/Fatigue")]
    public class Fatigue : Stat
    {
        #region Variables
        [Header("Fatigue Movement Variables")]
        [SerializeField] private float m_RunningFatigueValue;
        [SerializeField] private float m_WalkingFatigueValue;
        [SerializeField] private float m_CrouchFatigueValue;
        [SerializeField] private float m_JumpingFatigueValue;

        [Header("Fatigue Durations")]
        [SerializeField] private float m_IncreaseDuration = 4;
        [SerializeField] private float m_DecreaseDuration = 4;
        [SerializeField] private float m_MovementUpdateDuration = 2f;

        [Header("Fatigue Values")]
        [SerializeField] private float m_DecreaseAmount;
        [SerializeField] private float m_IncreaseAmount;

        private float m_Timer;
        private float m_FatigueMovementTimer;
        private const float m_Threshold = 0.15f;
        #endregion

        #region Property
        internal override float Modify
        {
            get => base.Modify;
            set => base.Modify = value;
        }
        #endregion

        public void UpdateFatigue()
        {
            Stamina stamina = StatManager.Instance.GetStat<Stamina>();
            if (CheckStaminaStatus(stamina))
                DecreaseFatigue();
            else
                IncreaseFatigue();
        }

        public void DecreaseFatigue()
        {
            m_Timer += Time.deltaTime;
            if (m_Timer >= m_DecreaseDuration)
            {
                Modify -= IsValueInRange(m_Threshold, m_DecreaseAmount);
                m_Timer = 0.0f;
            }
        }

        public void DecreaseFatigue(MovementType type)
        {
            m_FatigueMovementTimer += Time.deltaTime;
            if (CheckFatigueStatus())
            {
                DecreaseMovementFatigue(type);
                m_FatigueMovementTimer = 0.0f;
            }
        }

        public void IncreaseFatigue()
        {
            m_Timer += Time.deltaTime;
            if (m_Timer >= m_IncreaseDuration)
            {
                Modify += IsValueInRange(m_Threshold, m_IncreaseAmount);
                m_Timer = 0.0f;
            }
        }

        private void DecreaseMovementFatigue(MovementType type)
        {
            switch (type)
            {
                case MovementType.Walk:
                    Modify -= IsValueInRange(m_Threshold, m_WalkingFatigueValue);
                    break;
                case MovementType.Run:
                    Modify -= IsValueInRange(m_Threshold, m_RunningFatigueValue);
                    break;
                case MovementType.Crouch:
                    Modify -= IsValueInRange(m_Threshold, m_CrouchFatigueValue);
                    break;
                case MovementType.Jump:
                    Modify -= IsValueInRange(m_Threshold, m_JumpingFatigueValue);
                    break;
            }
        }

        private bool CheckFatigueStatus()
            => m_FatigueMovementTimer >= m_MovementUpdateDuration;

        private bool CheckStaminaStatus(Stamina stamina)
            => stamina.CurrentValue < stamina.MaxValue;

        private float IsValueInRange(float min, float max)
            => Random.Range(min, max);
    }
}
