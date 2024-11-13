using Core;
using UnityEngine;
using Utils;

namespace UI
{
    public class HorizontalMenu : MonoBehaviour
    {
        #region Protected fields
        [SerializeField]
        protected HorizontalMenuButton[] _buttons;

        protected InputReceiver _inputReceiver;
        protected int _cursorPosition;
        #endregion

        #region Properties
        public int CursorPosition
        {
            get { return _cursorPosition; }
        }

        public HorizontalMenuButton[] Buttons
        {
            get { return _buttons; }
        }
        #endregion

        #region Unity Methods
        protected virtual void Start()
        {
            BindInputs();
            Init();
        }
        #endregion

        #region Methods
        protected void BindInputs()
        {
            _inputReceiver = GetComponent<InputReceiver>();

            _inputReceiver.OnMoveLeft.AddListener(() =>
            {
                if (CanReceiveInput())
                {
                    CommonSounds.CursorMoved();
                    MoveCursorLeft();
                }
            });

            _inputReceiver.OnMoveRight.AddListener(() =>
            {
                if (CanReceiveInput())
                {
                    CommonSounds.CursorMoved();
                    MoveCursorRight();
                }
            });

            _inputReceiver.OnSelect.AddListener(() =>
            {
                if (CanReceiveInput())
                {
                    CommonSounds.OptionSelected();
                    SelectButton();
                }
            });
        }

        protected virtual bool CanReceiveInput() { return true; }

        public virtual void Init()
        {
            _cursorPosition = 0;

            UpdateCursorPosition();
        }

        private void MoveCursorLeft()
        {
            _cursorPosition = _cursorPosition == 0 ? _buttons.Length - 1 : --_cursorPosition;
            UpdateCursorPosition();
        }

        private void MoveCursorRight()
        {
            _cursorPosition = _cursorPosition == _buttons.Length - 1 ? 0 : ++_cursorPosition;
            UpdateCursorPosition();
        }

        private void UpdateCursorPosition()
        {
            for (int i = 0; i < _buttons.Length; i++)
            {
                _buttons[i].HoverButton(i == _cursorPosition);
            }
        }

        private void SelectButton()
        {
            _buttons[_cursorPosition].SelectButton();
        }

        public void FeedButtons(HorizontalMenuButton[] buttons)
        {
            _buttons = buttons;
        }
        #endregion
    }
}
