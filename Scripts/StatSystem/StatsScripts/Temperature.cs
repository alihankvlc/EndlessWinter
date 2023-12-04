using UnityEngine;
namespace EndlessWinter.Stat
{
    [CreateAssetMenu(fileName = "Temperature", menuName = "EndlessWinter/CreateStat/Temperature")]
    public class Temperature : Stat
    {
        #region Temperature Variables
        [Space]
        [Header("Temperature Settings")]
        [SerializeField] private int m_InitialTemp;
        [SerializeField] private int m_minTemp;
        [Header("Environmental Factors")]
        [SerializeField] private int m_MaxTemp;
        /// <summary>
        /// ileride farklı ortamlara göre değişiklik gösterebilen soğuk hava etkisi şeysi olcak inş :D
        /// </summary>
        [SerializeField] private int m_ColdWeatherEffect;
        /// <summary>
        /// item sistemi henüz olmadığı için şimdilik bir default değer alabilir.
        /// </summary>
        [SerializeField] private int m_ClothingEffect = 5;
/// <summary>
/// ileride Her binanın farklı yalıtım seviyeleri vs olabilir şimdilik defaul kalıyor.
/// </summary>
        [SerializeField] private int m_InBuildingTempBonus = 10;
        #endregion
        #region Property
        public int BaseTemperature => m_InitialTemp;
        public int MinTemperature => m_minTemp;
        public int MaxTemperature => m_MaxTemp;
        public int ColdWeatherEffect => m_ColdWeatherEffect;
        public int ClothingEffect => m_ClothingEffect;
        public int InBuildingTempBonus => m_InBuildingTempBonus;
        internal override float Modify
        {
            get => base.Modify;
            set => base.Modify = value;
        }
        #endregion
    }
}
