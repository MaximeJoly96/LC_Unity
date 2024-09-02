using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Language;
using Core;
using Inputs;
using System.Linq;
using UnityEngine.Events;
using Utils;

namespace MsgBox
{
    public enum MessageBoxType
    {
        Information,
        Warning
    }

    public enum MessageBoxAnswerType
    {
        Ok,
        OkCancel,
        YesNo
    }

    public enum MessageBoxAnswer
    {
        Ok,
        Yes,
        No,
        Cancel
    }

    public class MessageBox : MonoBehaviour
    {
        private const float SELECTION_DELAY = 0.2f;

        [SerializeField]
        private TMP_Text _boxTitle;
        [SerializeField]
        private Image _iconLeft;
        [SerializeField]
        private Image _iconRight;

        [SerializeField]
        private TMP_Text _message;
        [SerializeField]
        private MessageBoxButtonsSet[] _buttonsSets;

        private bool _busy;
        private float _selectionDelay;
        private int _cursorPosition;
        private MessageBoxAnswerType _currentAnswerType;
        private MessageBoxAnswer _result;

        private UnityEvent<MessageBoxAnswer> _messageBoxClosedWithResult;
        public UnityEvent<MessageBoxAnswer> MessageBoxClosedWithResult
        {
            get
            {
                if (_messageBoxClosedWithResult == null)
                    _messageBoxClosedWithResult = new UnityEvent<MessageBoxAnswer>();

                return _messageBoxClosedWithResult;
            }
        }

        private void Start()
        {
            FindObjectOfType<InputController>().ButtonClicked.AddListener(HandleInputs);
        }

        public void Show(string message, MessageBoxAnswerType answerType, MessageBoxType messageType)
        {
            _cursorPosition = 0;
            GlobalStateMachine.Instance.UpdateState(GlobalStateMachine.State.OpeningMessageBox);

            _currentAnswerType = answerType;
            _message.text = message;

            string titleKey = "";
            switch(messageType)
            {
                case MessageBoxType.Warning:
                    titleKey = "warningLabel";
                    break;
                case MessageBoxType.Information:
                    titleKey = "informationLabel";
                    break;
            }

            _boxTitle.text = Localizer.Instance.GetString(titleKey);

            _iconLeft.gameObject.SetActive(messageType == MessageBoxType.Warning);
            _iconLeft.gameObject.SetActive(messageType == MessageBoxType.Warning);

            foreach(MessageBoxButtonsSet set in  _buttonsSets)
            {
                set.gameObject.SetActive(set.AnswerType == answerType);
            }

            UpdateCursor();
            GetComponent<Animator>().Play("ShowMessageBox");
        }

        public void Close()
        {
            GlobalStateMachine.Instance.UpdateState(GlobalStateMachine.State.ClosingMessageBox);
            GetComponent<Animator>().Play("CloseMessageBox");
        }

        public void FinishedOpening()
        {
            GlobalStateMachine.Instance.UpdateState(GlobalStateMachine.State.InMessageBox);
        }

        public void FinishedClosing()
        {
            MessageBoxClosedWithResult.Invoke(_result);
        }

        private void HandleInputs(InputAction input)
        {
            if(!_busy && GlobalStateMachine.Instance.CurrentState == GlobalStateMachine.State.InMessageBox)
            {
                switch(input)
                {
                    case InputAction.MoveLeft:
                        CommonSounds.CursorMoved();
                        MoveLeft();
                        break;
                    case InputAction.MoveRight:
                        CommonSounds.CursorMoved();
                        MoveRight();
                        break;
                    case InputAction.Select:
                        CommonSounds.OptionSelected();
                        SelectAnswer();
                        break;
                }

                _busy = true;
            }
        }

        protected void Update()
        {
            if (_busy)
            {
                _selectionDelay += Time.deltaTime;
                if (_selectionDelay > SELECTION_DELAY)
                {
                    _selectionDelay = 0.0f;
                    _busy = false;
                }
            }
        }

        private void MoveLeft()
        {
            switch(_currentAnswerType)
            {
                case MessageBoxAnswerType.Ok:
                    return;
                case MessageBoxAnswerType.OkCancel:
                case MessageBoxAnswerType.YesNo:
                    _cursorPosition = _cursorPosition == 0 ? 1 : 0;
                    break;
            }

            UpdateCursor();
        }

        private void MoveRight()
        {
            switch (_currentAnswerType)
            {
                case MessageBoxAnswerType.Ok:
                    return;
                case MessageBoxAnswerType.OkCancel:
                case MessageBoxAnswerType.YesNo:
                    _cursorPosition = _cursorPosition == 1 ? 0 : 1;
                    break;
            }

            UpdateCursor();
        }

        private void SelectAnswer()
        {
            _result = _buttonsSets.FirstOrDefault(b => b.AnswerType == _currentAnswerType).Select(_cursorPosition);
            Close();
        }

        private void UpdateCursor()
        {
            _buttonsSets.FirstOrDefault(b => b.AnswerType == _currentAnswerType).UpdateCursor(_cursorPosition);
        }
    }
}
