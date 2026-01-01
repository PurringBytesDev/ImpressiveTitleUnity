using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ItRevival
{
    public class AvatarCreator : MonoBehaviour
    {
        // left unity core in case we need it
        // === Settable Fields ===
        /*
        [SerializeField] private int privateValue = 0;   // Visible in Inspector
        public float publicValue = 1.0f;                 // Also visible
        protected bool protectedValue = false;           // Not visible
        */
        [Header("Body Parts")]
        public GameObject mainBody;
        public List<GameObject> heads;
        public List<GameObject> tails;

        [Header("UI Elements")]
        public TMP_Dropdown headDropdown;
        public TMP_Dropdown tailDropdown;
        public TMP_InputField avatarNameInput;
        public Button saveButton;
        public Button loadButton;

        private GameObject currentHead;
        private GameObject currentTail;

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
            PopulateDropdowns();

            headDropdown.onValueChanged.AddListener(SwapHead);
            tailDropdown.onValueChanged.AddListener(SwapTail);
            saveButton.onClick.AddListener(() => SaveAvatar(avatarNameInput.text));
            loadButton.onClick.AddListener(() => LoadAvatar(avatarNameInput.text));
        }

        // early prototype.
        void PopulateDropdowns()
        {
            headDropdown.ClearOptions();
            tailDropdown.ClearOptions();

            List<string> headOptions = new List<string>();
            List<string> tailOptions = new List<string>();

            for (int i = 0; i < heads.Count; i++)
                headOptions.Add("Head " + (i + 1));

            for (int i = 0; i < tails.Count; i++)
                tailOptions.Add("Tail " + (i + 1));

            headDropdown.AddOptions(headOptions);
            tailDropdown.AddOptions(tailOptions);
        }

        public void SwapHead(int index)
        {
            if (index < 0 || index >= heads.Count) return;
            if (currentHead) currentHead.SetActive(false);
            currentHead = heads[index];
            currentHead.SetActive(true);
            SyncWithBody(currentHead);
        }

        public void SwapTail(int index)
        {
            if (index < 0 || index >= tails.Count) return;
            if (currentTail) currentTail.SetActive(false);
            currentTail = tails[index];
            currentTail.SetActive(true);
            SyncWithBody(currentTail);
        }

        private void SyncWithBody(GameObject part)
        {
            var animator = part.GetComponent<Animator>();
            if (animator != null)
            {
                animator.runtimeAnimatorController = mainBody.GetComponent<Animator>().runtimeAnimatorController;
            }
        }

        public void SaveAvatar(string avatarName)
        {
            PlayerPrefs.SetInt(avatarName + "_head", heads.IndexOf(currentHead));
            PlayerPrefs.SetInt(avatarName + "_tail", tails.IndexOf(currentTail));
            PlayerPrefs.Save();
        }

        public void LoadAvatar(string avatarName)
        {
            int headIndex = PlayerPrefs.GetInt(avatarName + "_head", 0);
            int tailIndex = PlayerPrefs.GetInt(avatarName + "_tail", 0);
            SwapHead(headIndex);
            SwapTail(tailIndex);
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
