using System;
using UnityEngine;

namespace Charlles
{
    [RequireComponent(typeof(Rigidbody))]
    public class Character : MonoBehaviour
    {
        [Header("Base")]
        [SerializeField] protected float m_Speed = 5;
        [SerializeField] private float m_LerpLook = 5;
        [SerializeField] private Rigidbody m_Rigidbody;
        [SerializeField] protected Animator m_Animator;
        [SerializeField] private Transform m_Model;

        private Vector3 _currentDirection;
        private Vector3 _direction;
        protected float currentSpeed = 5;

        void Start()
        {
            currentSpeed = m_Speed;
        }

        void OnValidate()
        {
            m_Rigidbody = GetComponent<Rigidbody>();
        }

        public virtual void Move(Vector2 direction)
        {
            _direction.x = direction.x;
            _direction.z = direction.y;
        }

        private void FixedUpdate()
        {
            _direction = _direction.normalized * currentSpeed;
            _currentDirection = Vector3.Lerp(_currentDirection, _direction, m_LerpLook * Time.deltaTime);
            _currentDirection.y = m_Rigidbody.velocity.y;

            m_Rigidbody.velocity = _currentDirection;
            m_Animator.SetFloat("Speed", m_Rigidbody.velocity.magnitude / currentSpeed);

            LookTo(_currentDirection);
        }

        public virtual void LookTo(Vector3 direction)
        {

            direction.y = 0;

            if (direction == Vector3.zero || direction.magnitude < 0.1f)
                return;


            Quaternion targetRotation = Quaternion.LookRotation(direction);

            m_Model.rotation = Quaternion.Lerp(m_Model.rotation, targetRotation, m_LerpLook * Time.deltaTime);

        }
    }
}