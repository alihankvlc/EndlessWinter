using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    [SerializeField] private PlayerInput m_PlayerInput;

    private InputActionMap m_InputActionMap;
    private InputAction m_WalkInputAction;
    private InputAction m_LookInputAction;
    private InputAction m_RunInputAction;
    private InputAction m_JumpInputAction;
    private InputAction m_CrouchInputAction;
<<<<<<< HEAD
    private InputAction m_ChangeViewInputAction;
    private InputAction m_WatchViewedInputAction;

    public Vector2 move { get; private set; }
    public Vector2 look { get; private set; }
    public bool run { get; private set; }
    public bool jump { get; private set; }
    public bool crouch { get; private set; }
    public bool changeView { get; private set; }
    public bool watchViewed { get; private set; }
=======

    public Vector2 Walk { get; private set; }
    public Vector2 Look { get; private set; }
    public bool Run { get; private set; }
    public bool Jump { get; private set; }
    public bool Crouch { get; private set; }
>>>>>>> parent of 3211f14 (Update)

    private void Awake()
    {
        m_InputActionMap = m_PlayerInput.currentActionMap;

        m_WalkInputAction = m_InputActionMap.FindAction("Walk");
        m_LookInputAction = m_InputActionMap.FindAction("Look");
        m_RunInputAction = m_InputActionMap.FindAction("Run");
        m_JumpInputAction = m_InputActionMap.FindAction("Jump");
        m_CrouchInputAction = m_InputActionMap.FindAction("Crouch");
<<<<<<< HEAD
        m_ChangeViewInputAction = m_InputActionMap.FindAction("ChangeViewMode");
        m_WatchViewedInputAction = m_InputActionMap.FindAction("WatchViewed");
=======
>>>>>>> parent of 3211f14 (Update)
    }
    private void OnEnable() => m_InputActionMap.Enable();
    private void OnDisable() => m_InputActionMap.Disable();
    private void Start()
    {
        m_WalkInputAction.performed += OnWalk;
        m_WalkInputAction.canceled += OnWalk;

        m_LookInputAction.performed += OnLook;
        m_LookInputAction.canceled += OnLook;

        m_RunInputAction.performed += OnRun;
        m_RunInputAction.canceled += OnRun;

        m_CrouchInputAction.performed += OnCrouch;
        m_CrouchInputAction.canceled += OnCrouch;

        m_JumpInputAction.started += OnJump;
        m_JumpInputAction.canceled += OnJump;
<<<<<<< HEAD

        m_ChangeViewInputAction.started += OnChangeViewMode;

        m_WatchViewedInputAction.performed += OnWatchViewed;
        m_WatchViewedInputAction.canceled += OnWatchViewed;
    }
    private void OnMove(InputAction.CallbackContext context) => move = context.ReadValue<Vector2>();
    private void OnLook(InputAction.CallbackContext context) => look = context.ReadValue<Vector2>();
    private void OnRun(InputAction.CallbackContext context) => run = context.ReadValueAsButton();
    private void OnCrouch(InputAction.CallbackContext context) => crouch = context.ReadValueAsButton();
    private void OnJump(InputAction.CallbackContext context) => jump = context.ReadValueAsButton();
    private void OnWatchViewed(InputAction.CallbackContext context) => watchViewed = context.ReadValueAsButton();
    private void OnChangeViewMode(InputAction.CallbackContext context) => changeView = !changeView;
=======
    }
    private void OnWalk(InputAction.CallbackContext context) => Walk = context.ReadValue<Vector2>();
    private void OnLook(InputAction.CallbackContext context) => Look = context.ReadValue<Vector2>();
    private void OnRun(InputAction.CallbackContext context) => Run = context.ReadValueAsButton();
    private void OnCrouch(InputAction.CallbackContext context) => Crouch = context.ReadValueAsButton();
    private void OnJump(InputAction.CallbackContext context) => Jump = context.ReadValueAsButton();
>>>>>>> parent of 3211f14 (Update)
}
