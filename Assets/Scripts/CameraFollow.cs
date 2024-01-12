using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public float smoothSpeed = 0f;
    private Vector3 offset = new Vector3(0, 14.0f, -5.0f);
    void LateUpdate() {
        Vector3 desiredPosition = target.position + offset;
        transform.position = desiredPosition;
        transform.LookAt(target);
    }
}
