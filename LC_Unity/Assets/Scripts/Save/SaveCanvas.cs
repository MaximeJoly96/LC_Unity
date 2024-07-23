using UnityEngine;
using TMPro;
using Inputs;

namespace Save
{
    public class SaveCanvas : MonoBehaviour
    {
        private const float SELECTION_DELAY = 0.2f; // seconds

        [SerializeField]
        private TMP_Text _tooltip;
        [SerializeField]
        private Transform _savesWrapper;
        [SerializeField]
        private SaveSlot _saveSlotPrefab;

        private float _selectionDelay;
        private bool _delayOn;
        private bool _isOpen;

        public CanvasGroup CanvasGroup { get { return GetComponent<CanvasGroup>(); } }
        public Animator Animator { get { return GetComponent<Animator>(); } }

        private void Awake()
        {
            DontDestroyOnLoad(gameObject);

            _selectionDelay = 0.0f;
            _delayOn = false;
        }

        private void Start()
        {
            FindObjectOfType<InputController>().ButtonClicked.AddListener(ReceiveInput);
        }

        public void Close()
        {
            _isOpen = false;
            Animator.Play("CloseSaveWindow");
        }

        public void FinishedClosing()
        {
            SaveManager.Instance.LoadPreviousState();
        }

        public void Open()
        {
            Animator.Play("OpenSaveWindow");
        }

        public void FinishedOpening()
        {
            _isOpen = true;
        }

        public void UpdateTooltip(string text)
        {
            _tooltip.text = text;
        }

        private void ReceiveInput(InputAction input)
        {
            if(!_delayOn && _isOpen)
            {
                switch (input)
                {
                    case InputAction.Cancel:
                        Close();
                        break;
                }
            }
        }

        protected void Update()
        {
            if (_delayOn)
            {
                _selectionDelay += Time.deltaTime;
                if (_selectionDelay > SELECTION_DELAY)
                {
                    _selectionDelay = 0.0f;
                    _delayOn = false;
                }
            }
        }
    }
}
