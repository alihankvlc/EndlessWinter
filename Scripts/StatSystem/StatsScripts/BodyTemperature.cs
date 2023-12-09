namespace EndlessWinter.Stat
{
    using EndlessWinter.Item;
    using EndlessWinter.Manager;
    using System.Collections.Generic;
    using UnityEngine;
    /*
     
     
     Düzenlenecek...

    Algoritma&Denklem daha düzgün kurabilirdim :(
     
     */
    [CreateAssetMenu(fileName = "Temperature", menuName = "EndlessWinter/CreateStat/Temperature")]
    public class BodyTemperature : Stat
    {
        [Header("Temperature Bonuses")]
        [SerializeField] internal float BuildingInTempBonus;
        [SerializeField] internal float IsFireTempBonus;
        [Range(1f, 5f), SerializeField] internal float DecreaseTempdivisor;

        [Header("Clothes Bonuses (Inactive)")]
        [SerializeField] internal List<Clothes> ClothesTempBonus = new List<Clothes>();

        [Header("Environment Checks")]
        [SerializeField] private bool m_IsInBuilding;
        [SerializeField] private bool m_IsInFire;

        private float m_RandomDecreaseTotalValue;
        private float m_DecreaseAmount;
        private float m_BodyTemperature;

        public void UpdateBodyTemperature()
        {
            EnvironmentTemp();
            ApplyTemperatureChanges();
        }
        private void ApplyTemperatureChanges()
        {
            float randomDecrease = m_BodyTemperature > 0
            ? Random.Range(0, m_BodyTemperature) : Random.Range(m_BodyTemperature, 0);

            m_DecreaseAmount += randomDecrease / DecreaseTempdivisor;
            m_RandomDecreaseTotalValue = randomDecrease;
            if (m_DecreaseAmount <= m_RandomDecreaseTotalValue)
            {
                Modify += m_DecreaseAmount;
                m_DecreaseAmount = 0;
            }
        }
        private void EnvironmentTemp()
        {
            float globalTemperature = GameManager.Instance.Temperature;
            m_BodyTemperature = globalTemperature +
                (m_IsInBuilding ? BuildingInTempBonus : 0) + (m_IsInFire ? IsFireTempBonus : 0);
        }
    }
}
