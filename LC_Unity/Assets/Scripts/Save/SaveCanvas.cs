using UnityEngine;
using TMPro;
using Inputs;
using System.Collections.Generic;
using UnityEngine.UI;
using MsgBox;
using Language;
using Core;
using Utils;

namespace Save
{
    public class SaveCanvas : MonoBehaviour
    {
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

        private bool _isOpen;
        private int _cursorPosition;

        private List<SaveSlot> _instSaveSlots;
        private InputReceiver _inputReceiver;

        public CanvasGroup CanvasGroup { get { return GetComponent<CanvasGroup>(); } }
        public Animator Animator { get { return GetComponent<Animator>(); } }
        public TMP_Text Tooltip { get { return _tooltip; } }
        public Transform SavesWrapper { get { return _savesWrapper; } }
        public SaveSlot SaveSlotPrefab { get { return _saveSlotPrefab; } }
        public ScrollRect ScrollView { get { return _scrollView; } }

        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
        }

        private void Start()
        {
            BindInputs();
        }

        private void BindInputs()
        {
            _inputReceiver = GetComponent<InputReceiver>();

            _inputReceiver.OnSelect.AddListener(() =>
            {
                if(CanReceiveInput())
                {
                    CommonSounds.OptionSelected();
                    SelectSlot();
                }
            });

            _inputReceiver.OnCancel.AddListener(() =>
            {
                if (CanReceiveInput())
                {
                    CommonSounds.ActionCancelled();
                    Close();
                }
            });

            _inputReceiver.OnMoveUp.AddListener(() =>
            {
                if (CanReceiveInput())
                {
                    CommonSounds.CursorMoved();
                    MoveUp();
                }
            });

            _inputReceiver.OnMoveDown.AddListener(() =>
            {
                if (CanReceiveInput())
                {
                    CommonSounds.CursorMoved();
                    MoveDown();
                }
            });
        }

        private bool CanReceiveInput()
        {
            return _isOpen && GlobalStateMachine.Instance.CurrentState == GlobalStateMachine.State.SaveMenu;
        }

        public void Close()
        {
            _isOpen = false;

            if(Animator)
                Animator.Play("CloseSaveWindow");
        }

        public void FinishedClosing()
        {
            SaveManager.Instance.LoadPreviousState();
        }

        public void Open()
        {
            _cursorPosition = 0;

            if(Animator)
                Animator.Play("OpenSaveWindow");

            LoadSaveSlots();
            UpdateCursor(false);
            ScrollView.verticalNormalizedPosition = 1.0f;
        }

        public void FinishedOpening()
        {
             _isOpen = true;
        }

        public void UpdateTooltip(string text)
        {
            Tooltip.text = text;
        }

        private void LoadSaveSlots()
        {
            ClearSlots();

            for(int i = 0; i < MAX_SAVES;  i++)
            {
                SaveSlot slot = Instantiate(SaveSlotPrefab, SavesWrapper);
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

            foreach(Transform child in SavesWrapper)
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
                ScrollView.verticalNormalizedPosition = 0.0f;
            else if (_cursorPosition >= 4)   
                ScrollView.verticalNormalizedPosition -= goingUp ? -1.0f * SLOT_MOVE_DELTA : SLOT_MOVE_DELTA;
            else
                ScrollView.verticalNormalizedPosition = 1.0f;
        }

        private void SelectSlot()
        {
            SelectSlot(_instSaveSlots[_cursorPosition]);
        }

        private void SelectSlot(SaveSlot slot)
        {
            switch (SaveManager.Instance.CurrentSaveState)
            {
                case SaveManager.SaveState.LoadSave:
                    if (slot.Data != null)
                        SaveManager.Instance.SlotSelected(_instSaveSlots.IndexOf(slot));
                    break;
                case SaveManager.SaveState.CreateSave:
                    if (slot.Data == null)
                        SaveManager.Instance.SlotSelected(_instSaveSlots.IndexOf(slot));
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

        public void SetComponents(TMP_Text tooltip, Transform savesWrapper, SaveSlot saveSlotPrefab, ScrollRect scrollRect)
        {
            _tooltip = tooltip;
            _savesWrapper = savesWrapper;
            _saveSlotPrefab = saveSlotPrefab;
            _scrollView = scrollRect;
        }
    }
}
