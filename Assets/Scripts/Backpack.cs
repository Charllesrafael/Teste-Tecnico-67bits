using System;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

namespace Charlles
{
    public class Backpack : MonoBehaviour
    {
        [SerializeField] private Transform m_backpackPlayer;
        [SerializeField] private Transform m_packagePrefab;
        [Range(0f, 1f)]
        [SerializeField] private float m_LerpMove = 1f;
        [SerializeField] private float m_MinDistance = 0.753f;

        private Vector3 lastPositionTarget;

        [SerializeField] private List<Transform> m_packs;

        void Start()
        {
            m_packs = new List<Transform>();
            m_packs.Add(m_backpackPlayer);
        }

        void FixedUpdate()
        {
            if (m_packs.Count <= 1)
                return;

            //if (lastPositionTarget != m_packs[0].position)
            //{
            for (int i = 1; i < m_packs.Count; i++)
            {
                MoveUpdate(m_packs[i], m_packs[i - 1]);
                RotationUpdate(m_packs[i], m_packs[i - 1]);
            }
            lastPositionTarget = m_packs[0].position;
            //}
        }

        private void MoveUpdate(Transform myTransform, Transform target)
        {
            myTransform.position = Vector3.Lerp(myTransform.position, target.position + (Vector3.up * m_MinDistance), m_LerpMove);
        }

        private void RotationUpdate(Transform myTransform, Transform target)
        {
            myTransform.rotation = Quaternion.FromToRotation(-Vector3.up, (target.position - myTransform.position).normalized);
        }

        public void NewPackage()
        {
            Transform currentPackage = Instantiate(m_packagePrefab, m_packs[^1].position, m_packs[^1].rotation);
            currentPackage.position = m_packs[^1].position + (Vector3.up * m_MinDistance);
            m_packs.Add(currentPackage.transform);
        }

        internal void Clean()
        {
            for (int i = 1; i < m_packs.Count; i++)
            {
                Destroy(m_packs[i].gameObject);
            }
            m_packs.Clear();
            m_packs.Add(m_backpackPlayer);
        }

        internal int GetPackgeCount()
        {
            return m_packs.Count - 1;
        }
    }
}
