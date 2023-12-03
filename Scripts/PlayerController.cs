using UnityEngine.InputSystem;
using UnityEngine;
using Player.Stat;
using UnityEngine.Rendering.Universal;
using System.Collections;
using System;
using UnityEngine.Animations.Rigging;

namespace Player
{
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(CharacterController))]
    [RequireComponent(typeof(PlayerInput))]
    [RequireComponent(typeof(InputManager))]
    public class PlayerController : MonoBehaviour
    {
        #region PlayerMovementVariables
        [Header("Player Movement Variables")]
        [Min(1f)]
        [SerializeField]
        private float m_WalkSpeed;
        [Min(3f)][SerializeField] private float m_RunSpeed;
        [Min(1f)][SerializeField] private float m_SpeedChangeOffset;
        [Range(0.15f, 50f)][SerializeField] private float m_MouseSensitivity;
        private float m_TargetSpeed;
        private float m_Speed;
        private bool m_CanRunning;
        #endregion
        #region PlayerStatsVariables
        [Header("Player Stats Variables")]
        [Header("Stamina")]
        [SerializeField]
        private float m_StaminaDecreaseAmount;
        [SerializeField] private float m_StaminaIncreaseAmount;
        [SerializeField] private float m_FatigueMultiply;
        private float m_StaminaValue;
        private float m_FatigueValue;
        #endregion
        #region CameraVariables
        [Header("Camera Variables")]
        [SerializeField]
        private Transform m_CameraRoot;
        [SerializeField] private GameObject m_ThirdPersonCamera;
        [SerializeField] private Transform m_TPSCameraTransform;
        [SerializeField] private Transform m_FPSCameraTransform;
        [Range(-100f, 100f)][SerializeField] private float m_CameraBottomClamp;
        [Range(-100f, 100f)][SerializeField] private float m_CameraUpperClamp;
        [Space]
        [Range(-150f, 150f)]
        [SerializeField]
        private float m_CameraSideClampMin;
        [Range(-150f, 150f)][SerializeField] private float m_CameraSideClampMax;
        private Camera m_CameraMain;
        private float m_TargetRotation;
        private float m_CameraTargetYaw;
        private float m_CameraTargetPitch;
        private float m_RotationSmoothTime = 0.12f;
        private float m_RotationCurrentVelocity;
        #endregion
        #region AnimationVariables

        private readonly int m_SpeedHash = Animator.StringToHash("Speed");
        private float m_MovementAnimBlend;

        #endregion
        #region OtherVariables
        [Header("Other")][SerializeField] private bool m_CursorLocked;
        [SerializeField] private ViewMode m_PlayerViewMode;
        [SerializeField] private UniversalRendererData Renderer;
        private const float m_Threshold = 0.1f;
        #endregion
        private CharacterController m_Controller;
        private Animator m_Animator;
        private InputManager m_Input;
        private void Start()
        {
            #region InitializeComponent

            m_Controller = GetComponent<CharacterController>();
            m_Animator = GetComponent<Animator>();
            m_Input = GetComponent<InputManager>();

            #endregion

            Cursor.lockState = m_CursorLocked ? CursorLockMode.Locked : CursorLockMode.None;
            Cursor.visible = Cursor.lockState == CursorLockMode.Locked ? false : true;
            m_CameraMain = Camera.main;
        }
        private void Update()
        {
            Movement();
            CameraRotation();
            SetChangeViewMode();

            m_PlayerViewMode = m_Input.changeView ? ViewMode.ThirdPerson : ViewMode.FirstPerson;
        }
        #region Funcs
        #region Movement
        private void Movement()
        {
            m_TargetSpeed = m_Input.move != Vector2.zero ? (m_Input.run & m_CanRunning ? m_RunSpeed : m_WalkSpeed) : 0;

            Vector3 inputDirection = new Vector3(m_Input.move.x, 0.0f,
                m_Input.move.y);

            float currentSpeed = new Vector3(m_Controller.velocity.x, 0.0f, m_Controller.velocity.z).magnitude;
            float speedOffset = m_Threshold;

            if (currentSpeed < m_TargetSpeed - speedOffset ||
                currentSpeed > m_TargetSpeed + speedOffset)
            {
                m_Speed = Mathf.MoveTowards
                    (currentSpeed, m_TargetSpeed * 1, Time.deltaTime * m_SpeedChangeOffset);
            }

            if (m_Input.move != Vector2.zero && !m_Input.watchViewed)
            {
                m_TargetRotation = Mathf.Atan2(inputDirection.x, inputDirection.z) * Mathf.Rad2Deg +
                                   m_CameraMain.transform.eulerAngles.y;

                float rotation = Mathf.SmoothDampAngle(transform.eulerAngles.y, m_TargetRotation,
                    ref m_RotationCurrentVelocity,
                    m_RotationSmoothTime);

                transform.rotation = Quaternion.Euler(0.0f, rotation, 0.0f);
            }

            Vector3 moveDirection = Quaternion.Euler(0.0f, m_TargetRotation, 0.0f) * Vector3.forward;
            moveDirection.Normalize();

            m_Controller.Move(moveDirection * m_Speed * Time.deltaTime);

            m_StaminaValue = StatManager.Instance.GetStat<Stamina>().CurrentValue;
            m_CanRunning = m_StaminaValue > 0;

            float staminaChangeAmount = 0f;

            /*Statlara StatManager instance üzerinden deðil direk stamina sýnýfýnýn örneðini üzerinden
            eriþebilirim.*/

            if (m_Input.run && m_CanRunning && m_Input.move != Vector2.zero)
            {
                staminaChangeAmount = -Time.deltaTime * StatManager.Instance.GetStat<Stamina>().DecreaseAmount;
            }
            else if (!m_Input.run && staminaChangeAmount != StatManager.Instance.GetStat<Stamina>().MaxValue)
            {
                staminaChangeAmount = Time.deltaTime * StatManager.Instance.GetStat<Stamina>().IncreaseAmount;
            }

            StatManager.Instance.GetStat<Stamina>().Modify += staminaChangeAmount;


            m_MovementAnimBlend = Mathf.MoveTowards(m_MovementAnimBlend,
                m_TargetSpeed, Time.deltaTime * m_SpeedChangeOffset);

            if (m_MovementAnimBlend < m_Threshold & m_Input.move == Vector2.zero)
                m_MovementAnimBlend = 0.0f;

            m_Animator.SetFloat(m_SpeedHash, m_MovementAnimBlend);
        }
        #endregion
        #region CameraRotation
        private void CameraRotation()
        {
            if (m_Input.look.sqrMagnitude > m_Threshold)
            {
                m_CameraTargetYaw += m_Input.look.x * m_MouseSensitivity * Time.deltaTime;
                m_CameraTargetPitch -= m_Input.look.y * m_MouseSensitivity * Time.deltaTime;

                m_CameraTargetPitch = Mathf.Clamp(m_CameraTargetPitch, m_CameraUpperClamp, m_CameraBottomClamp);
            }
            m_CameraRoot.transform.rotation = Quaternion.Euler(m_CameraTargetPitch, m_CameraTargetYaw, 0.0f);
        }
        #endregion
        private void SetChangeViewMode()
        {
            m_ThirdPersonCamera.SetActive(m_PlayerViewMode == ViewMode.ThirdPerson ? true : false);
            m_CameraRoot.transform.SetParent(
                m_PlayerViewMode == ViewMode.ThirdPerson ? m_TPSCameraTransform : m_FPSCameraTransform, false);

            Renderer.opaqueLayerMask = m_PlayerViewMode ==
                ViewMode.ThirdPerson ? (Renderer.opaqueLayerMask | LayerMask.GetMask("PlayerHead"))
                : (Renderer.opaqueLayerMask & ~LayerMask.GetMask("PlayerHead"));

        }
        #endregion
    }
}
