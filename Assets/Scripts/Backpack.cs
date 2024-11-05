using System;
using System.Collections.Generic;
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

        private List<Transform> packs;

        void Awake()
        {
            packs = new List<Transform>();
            packs.Add(m_backpackPlayer);
        }

        void FixedUpdate()
        {
            if (packs.Count <= 1)
                return;

            for (int i = 1; i < packs.Count; i++)
            {
                MoveUpdate(packs[i], packs[i - 1]);
                RotationUpdate(packs[i], packs[i - 1]);
            }
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
            Transform currentPackage = Instantiate(m_packagePrefab, packs[^1].position, packs[^1].rotation);
            currentPackage.position = packs[^1].position + (Vector3.up * m_MinDistance);
            packs.Add(currentPackage.transform);
        }

        internal void Clean()
        {
            for (int i = 1; i < packs.Count; i++)
            {
                Destroy(packs[i].gameObject);
            }
            packs.Clear();
            packs.Add(m_backpackPlayer);
        }

        internal int GetPackgeCount()
        {
            return packs.Count > 1 ? packs.Count - 1 : 0;
        }

        internal Transform GetBackpack()
        {
            return m_backpackPlayer;
        }
    }
}
