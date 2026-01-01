using UnityEngine;

namespace ItRevival
{
    public class Main : MonoBehaviour
    {
        public static string version = "0.0.1";

        public static bool IsServer = false;
        public static bool IsClient = true;

        // left unity core in case we need it
        // === Settable Fields ===
        [SerializeField] private int privateValue = 0;   // Visible in Inspector
        public float publicValue = 1.0f;                 // Also visible
        protected bool protectedValue = false;           // Not visible

        // === Unity Event Methods ===
        private void Reset()
        {
            // Called when the script is attached or reset
        }

        private void Awake()
        {
            // Called when the script instance is being loaded (before Start)
        }

        private void OnEnable()
        {
            // Called when the object becomes enabled and active
        }

        private void Start()
        {
            // Called before the first frame update (after Awake)
        }

        private void FixedUpdate()
        {
            // Called every fixed framerate frame (good for physics)
        }

        private void Update()
        {
            // Called once per frame
        }

        private void LateUpdate()
        {
            // Called after all Update functions have been called
        }

        private void OnGUI()
        {
            // Called for rendering and handling GUI events
        }

        private void OnDisable()
        {
            // Called when the behaviour becomes disabled
        }

        private void OnDestroy()
        {
            // Called before the object is destroyed
        }

        // === Common Unity Event Handlers ===
        private void OnTriggerEnter(Collider other)
        {
            // Called when another collider enters a trigger collider attached to this object
        }

        private void OnTriggerExit(Collider other)
        {
            // Called when another collider exits a trigger collider
        }

        private void OnCollisionEnter(Collision collision)
        {
            // Called when this collider/rigidbody has begun touching another rigidbody/collider
        }

        private void OnCollisionExit(Collision collision)
        {
            // Called when this collider/rigidbody has stopped touching another
        }

        private void OnMouseDown()
        {
            // Called when the user has pressed the mouse button while over the GUIElement or Collider
        }

        private void OnDrawGizmos()
        {
            // Called to draw gizmos in the editor
        }

        private void OnValidate()
        {
            // Called when a script is loaded or a value is changed in the Inspector
        }

        // === Utility Methods ===
        public void CustomFunction()
        {
            // You can call this from other scripts or via UnityEvent
        }
    }
}
