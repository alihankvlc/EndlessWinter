using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(PlayerInput))]
[RequireComponent(typeof(InputManager))]
public class Player : MonoBehaviour
{
    #region PlayerMovementVariables
    [Header("Player Movement Variables")]
    [SerializeField] private float m_WalkSpeed;
    [SerializeField] private float m_RunSpeed;
    [SerializeField] private float m_RotationSpeed;

    private float m_Speed;
    #endregion
    #region PlayerStatsVariables
    #endregion

    private CharacterController m_Controller;
    private Animator m_Animator;
    private InputManager m_InputManager;
    private StatsManager m_Stat;
    private void Start()
    {
        m_Controller = GetComponent<CharacterController>();
        m_Animator = GetComponent<Animator>();
        m_InputManager = GetComponent<InputManager>();

        m_Stat = new StatsManager();
    }
    private void Update()
    {

    }
}
