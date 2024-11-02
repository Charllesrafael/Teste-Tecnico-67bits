using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace Charlles
{
    public class Npc : Character
    {
        [SerializeField] private Collider npcCollider;
        [SerializeField] private Rigidbody currentRigidbody;
        private void OnTriggerEnter(Collider other)
        {
            m_Animator.enabled = false;
            npcCollider.enabled = false;
            currentRigidbody.isKinematic = true;
        }
    }
}
