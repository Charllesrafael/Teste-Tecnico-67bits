using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

namespace Charlles
{
    public class Store : MonoBehaviour
    {
        [SerializeField] private ParticleSystem m_sellEffect;
        private void OnTriggerEnter(Collider other)
        {
            if (GameManager.Instance.HasPackage())
            {
                m_sellEffect?.Play();
                GameManager.Instance.SellAll();
            }
        }
    }
}
