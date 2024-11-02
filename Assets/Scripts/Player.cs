using UnityEngine;
using UnityEngine.InputSystem;

namespace Charlles
{
    public class Player : Character
    {
        [Header("Player")]
        [SerializeField] private Collider punchDamage;
        [SerializeField] private float delayToActiveColliderPunching = 1f;
        [SerializeField] private float delayToMoveAfterActiveColliderPunching = 1f;
        [SerializeField] protected float m_PunchSpeed = 3;

        private PlayerInput m_PlayerInput;


        void OnEnable()
        {
            m_PlayerInput = new PlayerInput();
            m_PlayerInput.Player.Enable();

            m_PlayerInput.Player.Move.performed += OnMove;
            m_PlayerInput.Player.Move.canceled += OnMove;

            m_PlayerInput.Player.Punch.started += OnPunch;
            m_PlayerInput.Player.Punch.canceled += OnPunch;
        }

        void OnDisable()
        {
            m_PlayerInput.Player.Move.performed -= OnMove;
            m_PlayerInput.Player.Move.canceled -= OnMove;

            m_PlayerInput.Player.Punch.started -= OnPunch;
            m_PlayerInput.Player.Punch.canceled -= OnPunch;

            m_PlayerInput.Player.Disable();
        }

        public void OnMove(InputAction.CallbackContext context)
        {
            Move(context.ReadValue<Vector2>());
        }

        public void OnPunch(InputAction.CallbackContext context)
        {
            if (context.started)
            {
                m_Animator.SetTrigger("Puch");
                currentSpeed = m_PunchSpeed;

                Invoke(nameof(ActiveColliderPunch), delayToActiveColliderPunching);
            }
        }

        private void ActiveColliderPunch()
        {
            punchDamage.enabled = true;
            Invoke(nameof(OffPunch), delayToMoveAfterActiveColliderPunching);
        }

        private void OffPunch()
        {
            currentSpeed = m_Speed;
            punchDamage.enabled = false;
        }
    }
}