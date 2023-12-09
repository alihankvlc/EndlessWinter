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
    private InputAction m_ChangeViewInputAction;

    public Vector2 Move { get; private set; }
    public Vector2 Look { get; private set; }
    public bool Run { get; private set; }
    public bool Jump { get; private set; }
    public bool Crouch { get; private set; }
    public bool ChangeView { get; private set; }

    private void Awake()
    {
        m_InputActionMap = m_PlayerInput.currentActionMap;

        m_WalkInputAction = m_InputActionMap.FindAction("Walk");
        m_LookInputAction = m_InputActionMap.FindAction("Look");
        m_RunInputAction = m_InputActionMap.FindAction("Run");
        m_JumpInputAction = m_InputActionMap.FindAction("Jump");
        m_CrouchInputAction = m_InputActionMap.FindAction("Crouch");
        m_ChangeViewInputAction = m_InputActionMap.FindAction("ChangeViewMode");
    }
    private void OnEnable() => m_InputActionMap.Enable();
    private void OnDisable() => m_InputActionMap.Disable();
    private void Start()
    {
        m_WalkInputAction.performed += OnMove;
        m_WalkInputAction.canceled += OnMove;

        m_LookInputAction.performed += OnLook;
        m_LookInputAction.canceled += OnLook;

        m_RunInputAction.performed += OnRun;
        m_RunInputAction.canceled += OnRun;

        m_CrouchInputAction.performed += OnCrouch;
        m_CrouchInputAction.canceled += OnCrouch;

        m_JumpInputAction.started += OnJump;
        m_JumpInputAction.canceled += OnJump;

        m_ChangeViewInputAction.started += OnChangeViewMode;
    }
    private void OnMove(InputAction.CallbackContext context) => Move = context.ReadValue<Vector2>();
    private void OnLook(InputAction.CallbackContext context) => Look = context.ReadValue<Vector2>();
    private void OnRun(InputAction.CallbackContext context) => Run = context.ReadValueAsButton();
    private void OnCrouch(InputAction.CallbackContext context) => Crouch = context.ReadValueAsButton();
    private void OnJump(InputAction.CallbackContext context) => Jump = context.ReadValueAsButton();
    private void OnChangeViewMode(InputAction.CallbackContext context) => ChangeView = !ChangeView;
}
