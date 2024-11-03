using Unity.Mathematics;
using UnityEngine;

namespace Charlles
{
    public class Package : MonoBehaviour
    {
        [SerializeField] private Transform m_Target;
        [Range(0f, 1f)]
        [SerializeField] private float m_LerpMove = 1f;
        [SerializeField] private float m_LerpLook = 10f;
        [SerializeField] private float m_MinDistance = 0.753f;

        Vector3 direcao;

        void Start()
        {
            direcao = Vector3.zero;
            transform.position = m_Target.position + (Vector3.up * m_MinDistance);
        }

        void FixedUpdate()
        {
            MoveUpdate();
            RotationUpdate();
        }

        private void MoveUpdate()
        {
            //transform.position = Vector3.LerpUnclamped(transform.position, m_Target.position + (Vector3.up * m_MinDistance), m_LerpMove);
            transform.position = Vector3.Lerp(transform.position, m_Target.position + (Vector3.up * m_MinDistance), m_LerpMove);
        }

        private void RotationUpdate()
        {
            Quaternion targetRotation = Quaternion.LookRotation(m_Target.position - transform.position);

            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, m_LerpLook * Time.deltaTime);
        }

        public void SetTarget(Transform target)
        {
            m_Target = target;
        }
    }
}
