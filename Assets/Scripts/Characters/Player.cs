using UnityEngine;
using UnityEngine.InputSystem;

namespace Charlles
{
    public class Player : Character
    {
        [Header("Player")]
        [SerializeField] private Collider m_punchDamage;
        [SerializeField] private float m_delayToActiveColliderPunching = 1f;
        [SerializeField] private float m_delayToMoveAfterActiveColliderPunching = 1f;
        [SerializeField] protected float m_punchSpeed = 3;

        private PlayerInput playerInput;


        void OnEnable()
        {
            playerInput = new PlayerInput();
            playerInput.Player.Enable();

            playerInput.Player.Move.performed += OnMove;
            playerInput.Player.Move.canceled += OnMove;

            playerInput.Player.Punch.started += OnPunch;
            playerInput.Player.Punch.canceled += OnPunch;
        }

        void OnDisable()
        {
            playerInput.Player.Move.performed -= OnMove;
            playerInput.Player.Move.canceled -= OnMove;

            playerInput.Player.Punch.started -= OnPunch;
            playerInput.Player.Punch.canceled -= OnPunch;

            playerInput.Player.Disable();
        }

        public void OnMove(InputAction.CallbackContext context)
        {
            if (GameManager.Instance.IsPlaying())
                Move(context.ReadValue<Vector2>());
        }

        public void OnPunch(InputAction.CallbackContext context)
        {
            if (GameManager.Instance.IsPlaying())
            {
                if (context.started)
                {
                    m_Animator.SetTrigger("Puch");
                    currentSpeed = m_punchSpeed;

                    GameManager.Instance.PlayPunchSound();

                    Invoke(nameof(ActiveColliderPunch), m_delayToActiveColliderPunching);
                }
            }
        }

        private void ActiveColliderPunch()
        {
            m_punchDamage.enabled = true;
            Invoke(nameof(OffPunch), m_delayToMoveAfterActiveColliderPunching);
        }

        private void OffPunch()
        {
            currentSpeed = m_Speed;
            m_punchDamage.enabled = false;
        }
    }
}