using UnityEngine;
using TMPro;
using Inputs;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.UI;

namespace Save
{
    public class SaveCanvas : MonoBehaviour
    {
        private const float SELECTION_DELAY = 0.2f; // seconds
        private const int MAX_SAVES = 15;
        private const float SLOT_MOVE_DELTA = 0.095f;

        [SerializeField]
        private TMP_Text _tooltip;
        [SerializeField]
        private Transform _savesWrapper;
        [SerializeField]
        private SaveSlot _saveSlotPrefab;
        [SerializeField]
        private ScrollRect _scrollView;

        private float _selectionDelay;
        private bool _delayOn;
        private bool _isOpen;
        private int _cursorPosition;

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
            UpdateCursor();
            _scrollView.verticalNormalizedPosition = 0.0f;
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
                    case InputAction.MoveDown:
                        MoveDown();
                        break;
                    case InputAction.MoveUp:
                        MoveUp();
                        break;
                }

                _delayOn = true;
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

        private void MoveDown()
        {
            _cursorPosition = _cursorPosition == MAX_SAVES - 1 ? 0 : ++_cursorPosition;
            UpdateCursor();
        }

        private void MoveUp()
        {
            _cursorPosition = _cursorPosition == 0 ? MAX_SAVES - 1 : --_cursorPosition;
            UpdateCursor();
        }

        private void UpdateCursor()
        {
            for(int i = 0; i < _instSaveSlots.Count; i++)
            {
                if (i == _cursorPosition)
                    _instSaveSlots[i].Select();
                else
                    _instSaveSlots[i].Unselect();
            }

            _scrollView.verticalNormalizedPosition = 1 - Mathf.Clamp01(_cursorPosition * SLOT_MOVE_DELTA);
        }
    }
}
