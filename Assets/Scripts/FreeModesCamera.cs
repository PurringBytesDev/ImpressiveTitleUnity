using UnityEngine;

public class FreeModesCamera : MonoBehaviour
{
    public enum CameraMode
    {
        FreeMove,
        LockedBehind,
        // ThirdMode (to be added)
    }

    [Header("General")]
    public CameraMode currentMode = CameraMode.FreeMove;

    [Header("Free Move Mode Settings")]
    public float moveSpeed = 10f;
    public float lookSpeed = 2f;
    public float maxPitch = 80f;
    public float minPitch = -80f;

    [Header("Locked Behind Mode Settings")]
    public Transform target;  // The object to follow
    public Vector3 offset = new Vector3(0, 3, -5);
    public float positionSmoothTime = 0.3f;
    public float rotationSmoothTime = 0.3f;

    private Vector3 positionSmoothVelocity;
    private Vector3 currentRotationVelocity;
    private float pitch = 0f;
    private float yaw = 0f;

    void Start()
    {
        Vector3 angles = transform.eulerAngles;
        yaw = angles.y;
        pitch = angles.x;
    }

    void Update()
    {
        if (currentMode == CameraMode.FreeMove)
        {
            FreeMoveUpdate();
        }
        else if (currentMode == CameraMode.LockedBehind)
        {
            LockedBehindUpdate();
        }
    }

    void FreeMoveUpdate()
    {
        // Mouse look
        yaw += Input.GetAxis("Mouse X") * lookSpeed;
        pitch -= Input.GetAxis("Mouse Y") * lookSpeed;
        pitch = Mathf.Clamp(pitch, minPitch, maxPitch);

        transform.rotation = Quaternion.Euler(pitch, yaw, 0);

        // Keyboard movement (WASD)
        Vector3 inputDir = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        Vector3 moveDir = transform.rotation * inputDir;
        transform.position += moveDir * moveSpeed * Time.deltaTime;
    }

    void LockedBehindUpdate()
    {
        if (target == null)
            return;

        // Desired position
        Vector3 desiredPosition = target.position + target.rotation * offset;

        // Smooth position
        transform.position = Vector3.SmoothDamp(transform.position, desiredPosition, ref positionSmoothVelocity, positionSmoothTime);

        // Desired rotation - look at target smoothly
        Quaternion desiredRotation = Quaternion.LookRotation(target.position - transform.position, Vector3.up);

        transform.rotation = Quaternion.Lerp(transform.rotation, desiredRotation, Time.deltaTime / rotationSmoothTime);
    }

    // Optional: call this to switch modes externally
    public void SetMode(CameraMode mode)
    {
        currentMode = mode;
    }
}
