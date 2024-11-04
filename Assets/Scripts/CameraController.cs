using UnityEngine;

namespace Charlles
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField] private float m_speed;
        [SerializeField] private Transform m_target;

        void FixedUpdate()
        {
            transform.position = Vector3.Lerp(transform.position, m_target.position, m_speed * Time.deltaTime);
        }
    }
}
