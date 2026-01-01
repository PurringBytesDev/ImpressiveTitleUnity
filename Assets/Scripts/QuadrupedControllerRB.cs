using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(CapsuleCollider))]
public class QuadrupedControllerRB : MonoBehaviour
{
    public float walkSpeed = 2f;
    public float trotSpeed = 4f;
    public float runSpeed = 6f;
    public float turnSpeed = 5f;

    public float crouchHeight = 0.7f;
    public float crawlHeight = 0.5f;
    public float standHeight = 1.0f;

    public float colliderLength = 2.5f;
    public float transitionSpeed = 5f;

    public Animator animator;
    public Transform cameraTransform;

    private Rigidbody rb;
    private CapsuleCollider capsule;
    private float currentSpeed;
    private Vector3 targetCenter;

    private enum MovementState { Stand, Crouch, Crawl }
    private MovementState currentState = MovementState.Stand;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        capsule = GetComponent<CapsuleCollider>();

        rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;

        capsule.direction = 2; // Horizontal capsule along Z axis
        capsule.height = colliderLength;
        capsule.radius = 0.4f;
        capsule.center = new Vector3(0, standHeight / 2f, 0);
    }

    void Update()
    {
        HandleInput();
        Animate();
        AdjustCollider();
    }

    void FixedUpdate()
    {
        Move();
    }

    void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            if (currentState == MovementState.Stand)
                currentState = MovementState.Crouch;
            else if (currentState == MovementState.Crouch)
                currentState = MovementState.Crawl;
        }

        if (Input.GetKeyDown(KeyCode.V))
        {
            if (currentState == MovementState.Crawl)
                currentState = MovementState.Crouch;
            else if (currentState == MovementState.Crouch)
                currentState = MovementState.Stand;
        }

        // Set collider center based on state
        switch (currentState)
        {
            case MovementState.Stand:
                targetCenter = new Vector3(0, 0.5f, 0);
                break;
            case MovementState.Crouch:
                targetCenter = new Vector3(0, 0.35f, 0);
                break;
            case MovementState.Crawl:
                targetCenter = new Vector3(0, 0.25f, 0);
                break;
        }
    }

    void Move()
    {
        Vector3 input = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));
        Vector3 camForward = Vector3.Scale(cameraTransform.forward, new Vector3(1, 0, 1)).normalized;
        Vector3 move = input.z * camForward + input.x * cameraTransform.right;

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
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.fixedDeltaTime * turnSpeed);
        }

        currentSpeed = Mathf.Lerp(currentSpeed, targetSpeed, Time.fixedDeltaTime * 10f);
        Vector3 velocity = transform.forward * currentSpeed;
        velocity.y = rb.linearVelocity.y; // Preserve gravity

        rb.linearVelocity = velocity;
    }

    void AdjustCollider()
    {
        capsule.height = Mathf.Lerp(capsule.height, colliderLength, Time.deltaTime * transitionSpeed);
        capsule.center = Vector3.Lerp(capsule.center, targetCenter, Time.deltaTime * transitionSpeed);
    }

    void Animate()
    {
        float speedPercent = currentSpeed / runSpeed;
        animator.SetFloat("Speed", speedPercent, 0.1f, Time.deltaTime);
        animator.SetBool("IsCrouching", currentState == MovementState.Crouch);
        animator.SetBool("IsCrawling", currentState == MovementState.Crawl);
    }
}
