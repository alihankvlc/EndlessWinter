namespace EndlessWinter
{
    using System;
    using UnityEngine;
    using UnityEngine.UI;

    public class BackpackButtonEventArgs : MonoBehaviour
    {
        [SerializeField] private ItemCategoryType m_BackpackSlotType;
        private Button m_Button;
        public ItemCategoryType GetSlotType => m_BackpackSlotType;

        private void Awake()
            => m_Button = GetComponent<Button>();

        private void Start()
        {
            m_Button.onClick.AddListener(() =>
            {
                EventManager.Instance.TriggerEvent("IdentifyItemType", m_BackpackSlotType);
                EventManager.Instance.TriggerEvent("PlaySound", SoundType.ButtonClick);
            });
        }
    }
}
