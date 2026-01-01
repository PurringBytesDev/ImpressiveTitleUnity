using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public Vector3 offset = new Vector3(0f, 4f, -6f);
    public float smoothTime = 0.2f;

    private Vector3 velocity = Vector3.zero;

    void LateUpdate()
    {
        if (!target) return;

        Vector3 targetPos = target.position + target.TransformVector(offset);
        transform.position = Vector3.SmoothDamp(transform.position, targetPos, ref velocity, smoothTime);
        transform.LookAt(target);
    }
}
