using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class QuadrupedController : MonoBehaviour
{
    [Header("Movement Settings")]
    public float walkSpeed = 2f;
    public float trotSpeed = 4f;
    public float runSpeed = 6f;
    public float rotationSpeed = 8f;
    public float gravity = -9.81f;

    [Header("References")]
    public Transform camTransform;
    public Animator animator;

    private CharacterController controller;
    private Vector3 velocity;
    private float currentSpeed;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        if (camTransform == null)
            camTransform = Camera.main.transform;
    }

    void Update()
    {
        HandleMovement();
    }

    void HandleMovement()
    {
        Vector3 input = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));
        Vector3 camForward = Vector3.Scale(camTransform.forward, new Vector3(1, 0, 1)).normalized;
        Vector3 move = input.z * camForward + input.x * camTransform.right;

        float targetSpeed = 0f;
        if (input.magnitude > 0.1f)
        {
            if (Input.GetKey(KeyCode.LeftShift))
                targetSpeed = runSpeed;
            else if (Input.GetKey(KeyCode.LeftControl))
                targetSpeed = walkSpeed;
            else
                targetSpeed = trotSpeed;

            Quaternion targetRotation = Quaternion.LookRotation(move);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
        }

        currentSpeed = Mathf.Lerp(currentSpeed, targetSpeed, Time.deltaTime * 5f);
        Vector3 moveDir = transform.forward * currentSpeed;

        // Gravity
        if (controller.isGrounded)
            velocity.y = -1f;
        else
            velocity.y += gravity * Time.deltaTime;

        controller.Move((moveDir + velocity) * Time.deltaTime);

        // Animation
        float speedPercent = currentSpeed / runSpeed;
        animator.SetFloat("Speed", speedPercent, 0.1f, Time.deltaTime);
    }
}
