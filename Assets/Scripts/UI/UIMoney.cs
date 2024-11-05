using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Charlles
{
    public class UIMoney : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI m_textMesh;

        public void SetText(string text)
        {
            m_textMesh.text = text;
        }

        public void EndAnimation()
        {
            Destroy(gameObject);
        }
    }
}
