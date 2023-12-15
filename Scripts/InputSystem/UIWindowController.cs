using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

namespace EndlessWinter.InputController
{
    [RequireComponent(typeof(PlayerInput))]
    [RequireComponent(typeof(UIWindowController))]
    public class UIWindowController : Singleton<UIWindowController>
    {
        [SerializeField] private PlayerInput m_PlayerInput;

        #region InputAction
        private InputActionMap m_InputActionMap;
        private InputAction m_ShowInventoryInputAction;
        #endregion
        #region UI Window Variables
        [Header("UI Windows")]
        [SerializeField] private GameObject m_MainMenuWindow;
        [SerializeField] private GameObject m_InventoryWindow; 
        #endregion
        private bool m_InventoryWindowIsOpen;

        public bool InventoryWindowIsOpen => m_InventoryWindowIsOpen;
        protected override void Awake()
        {
            m_InputActionMap = m_PlayerInput.currentActionMap;
            #region InitializeInputAction
            m_ShowInventoryInputAction = m_InputActionMap.FindAction("ShowInventory");
            #endregion
        }
        private void OnEnable()
        {
            m_InputActionMap.Enable();
            #region InitializeInputActionCallbacks
            m_ShowInventoryInputAction.started += Show_Inventory;
            #endregion
        }
        private void OnDisable()
        {
            m_InputActionMap.Disable();
            #region RemoveInputActionCallbacks
            m_ShowInventoryInputAction.started -= Show_Inventory;
            #endregion
        }
        #region CallBackContextFuncs
        private void Show_Inventory(InputAction.CallbackContext context)
        {
            m_InventoryWindowIsOpen = !m_InventoryWindowIsOpen;

            m_MainMenuWindow.SetActive(m_InventoryWindowIsOpen);
            m_InventoryWindow.SetActive(m_InventoryWindowIsOpen);
        }
        #endregion
    }
}