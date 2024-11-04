using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Charlles
{
    public class Counter : MonoBehaviour
    {
        [SerializeField] private float m_speedCounter = 1;
        [SerializeField] private ParticleSystem m_effect;
        [SerializeField] private Transform m_baseCounter;

        private float currentState;

        private void OnTriggerStay(Collider other)
        {
            if (NotValid())
                return;

            m_baseCounter.localScale = Vector3.Lerp(Vector3.zero, Vector3.one, currentState);
            currentState += Time.deltaTime * m_speedCounter;

            if (currentState >= 1)
            {
                m_effect?.Play();
                Activate();
                m_baseCounter.localScale = Vector3.zero;
                currentState = 0;
            }
        }

        public virtual bool NotValid()
        {
            return true;
        }

        public virtual void Activate()
        {
        }

        private void OnTriggerExit(Collider other)
        {
            m_baseCounter.localScale = Vector3.zero;
            currentState = 0;
        }
    }
}
