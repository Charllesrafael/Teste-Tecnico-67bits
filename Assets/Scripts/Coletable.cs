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
        [SerializeField] private Vector3 OffSet;

        private void OnTriggerEnter(Collider other)
        {
            if (!GameManager.Instance.Backpackfull())
                Collect();
        }

        private void Collect()
        {
            m_Collider.enabled = false;
            if (m_coletableEffect)
                Instantiate(m_coletableEffect, transform.position + OffSet, Quaternion.identity);

            GameManager.Instance.AddNewPack();
            Destroy(m_parent);
        }
    }
}
