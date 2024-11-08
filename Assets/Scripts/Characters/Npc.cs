using UnityEngine;

namespace Charlles
{
    public class Npc : Character
    {
        [SerializeField] private float m_delayToActiveColiderColetable = 1;
        [SerializeField] private Collider m_npcCollider;
        [SerializeField] private Rigidbody m_currentRigidbody;
        [SerializeField] private Collider m_colliderColetable;

        private void OnTriggerEnter(Collider other)
        {
            m_Animator.enabled = false;
            m_npcCollider.enabled = false;
            m_currentRigidbody.isKinematic = true;
            GameManager.Instance.PlayImpactSound();

            Invoke(nameof(ActiveColiderColetable), m_delayToActiveColiderColetable);
        }

        private void ActiveColiderColetable()
        {
            m_colliderColetable.enabled = true;
        }
    }
}
