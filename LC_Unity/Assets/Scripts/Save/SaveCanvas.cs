using UnityEngine;
using TMPro;
using Inputs;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.UI;
using MsgBox;
using Language;
using Core;

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
            _cursorPosition = 0;
            Animator.Play("OpenSaveWindow");
            LoadSaveSlots();
            UpdateCursor(false);
            _scrollView.verticalNormalizedPosition = 1.0f;
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
            if(!_delayOn && _isOpen && GlobalStateMachine.Instance.CurrentState == GlobalStateMachine.State.SaveMenu)
            {
                switch (input)
                {
                    case InputAction.Cancel:
                        Close();
                        break;
                    case InputAction.Select:
                        SelectSlot();
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

            for(int i = 0; i < SaveManager.Instance.SavesId.Count; i++)
            {
                int slotId = SaveManager.Instance.SavesId[i];
                _instSaveSlots[slotId].Init(SaveManager.Instance.GetSavedDataFromSlot(slotId));
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
            UpdateCursor(false);
        }

        private void MoveUp()
        {
            _cursorPosition = _cursorPosition == 0 ? MAX_SAVES - 1 : --_cursorPosition;
            UpdateCursor(true);
        }

        private void UpdateCursor(bool goingUp)
        {
            for(int i = 0; i < _instSaveSlots.Count; i++)
            {
                if (i == _cursorPosition)
                    _instSaveSlots[i].Select();
                else
                    _instSaveSlots[i].Unselect();
            }

            if (_cursorPosition == MAX_SAVES - 1)
                _scrollView.verticalNormalizedPosition = 0.0f;
            else if (_cursorPosition >= 4)   
                _scrollView.verticalNormalizedPosition -= goingUp ? -1.0f * SLOT_MOVE_DELTA : SLOT_MOVE_DELTA;
            else
                _scrollView.verticalNormalizedPosition = 1.0f;
        }

        private void SelectSlot()
        {
            switch(SaveManager.Instance.CurrentSaveState)
            {
                case SaveManager.SaveState.LoadSave:
                    if (_instSaveSlots[_cursorPosition].Data != null)
                        SaveManager.Instance.SlotSelected(_cursorPosition);
                    break;
                case SaveManager.SaveState.CreateSave:
                    if (_instSaveSlots[_cursorPosition].Data == null)
                        SaveManager.Instance.SlotSelected(_cursorPosition);
                    else
                    {
                        MessageBoxService.Instance.MessageBoxClosedWithResult.RemoveAllListeners();
                        MessageBoxService.Instance.MessageBoxClosedWithResult.AddListener(ProceedWithSaveCreation);
                        MessageBoxService.Instance.ShowYesNoMessage(Localizer.Instance.GetString("saveDataAlreadyExists"), MessageBoxType.Warning);
                    }
                    break;
            }
        }

        private void ProceedWithSaveCreation(MessageBoxAnswer result)
        {
            if (result == MessageBoxAnswer.Yes)
                SaveManager.Instance.SlotSelected(_cursorPosition);
            else
                GlobalStateMachine.Instance.UpdateState(GlobalStateMachine.State.SaveMenu);
        }
    }
}
