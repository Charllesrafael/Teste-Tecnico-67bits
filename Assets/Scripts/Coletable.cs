using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Charlles
{
    public class Coletable : MonoBehaviour
    {
        [SerializeField] private GameObject m_parent;
        [SerializeField] private Collider m_Collider;
        [SerializeField] private GameObject m_coletableEffect;

        private void OnTriggerEnter(Collider other)
        {
            Collect();
        }

        private void Collect()
        {
            m_Collider.enabled = false;
            if (m_coletableEffect)
                Instantiate(m_coletableEffect, transform.position, Quaternion.identity);

            GameManager.Instance.AddNewPack();
            Destroy(m_parent);
        }
    }
}
