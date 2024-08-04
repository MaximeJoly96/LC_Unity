using UnityEngine;
using TMPro;
using Inputs;
using System.Collections.Generic;

namespace Save
{
    public class SaveCanvas : MonoBehaviour
    {
        private const float SELECTION_DELAY = 0.2f; // seconds
        private const int MAX_SAVES = 15;

        [SerializeField]
        private TMP_Text _tooltip;
        [SerializeField]
        private Transform _savesWrapper;
        [SerializeField]
        private SaveSlot _saveSlotPrefab;

        private float _selectionDelay;
        private bool _delayOn;
        private bool _isOpen;

        private List<SaveSlot> _instSaveSlots;

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
            LoadSaveSlots();
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
                    case InputAction.Select:
                        SaveManager.Instance.SlotSelected(0);
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

        private void LoadSaveSlots()
        {
            ClearSlots();

            for(int i = 0; i < MAX_SAVES;  i++)
            {
                SaveSlot slot = Instantiate(_saveSlotPrefab, _savesWrapper);
                _instSaveSlots.Add(slot);
            }

            for(int i = 0; i < SaveManager.Instance.SavesCount; i++)
            {
                _instSaveSlots[i].Init();
            }
        }

        private void ClearSlots()
        {
            if (_instSaveSlots == null)
                _instSaveSlots = new List<SaveSlot>();
            else
                _instSaveSlots.Clear();

            foreach(Transform child in _savesWrapper)
            {
                Destroy(child.gameObject);
            }
        }
    }
}
