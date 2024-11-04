using UnityEngine;

public class Example : MonoBehaviour
{
    void Update()
    {
        // Sets the rotation so that the transform's y-axis goes along the global y-axis and the transform's z-axis goes along the global z-axis
        transform.rotation *= Quaternion.FromToRotation(transform.up, Vector3.up);
        transform.rotation *= Quaternion.FromToRotation(transform.forward, Vector3.forward);
    }
}