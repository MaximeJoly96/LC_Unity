using Inputs;
using UnityEngine;
using UnityEngine.Events;
using System.Linq;
using Core;
using System.Collections.Generic;

namespace TitleScreen
{
    public class OptionsMenuPanel : TitleScreenPanel
    {
        [SerializeField]
        private SpecificTitleScreenOption[] _options;

        private UnityEvent _backButtonEvent;
        public UnityEvent BackButtonEvent
        {
            get
            {
                if (_backButtonEvent == null)
                    _backButtonEvent = new UnityEvent();

                return _backButtonEvent;
            }
        }

        protected override void Start()
        {
            base.Start();
            BackButton back = _options.FirstOrDefault(o => o is BackButton) as BackButton;

            if (back)
                back.BackButtonSelected.AddListener(() => BackButtonEvent.Invoke());
        }

        protected override void ReceiveInput(InputAction input)
        {
            if(!_delayOn && GlobalStateMachine.Instance.CurrentState == GlobalStateMachine.State.TitleScreenOptions)
            {
                switch (input)
                {
                    case InputAction.MoveDown:
                        MoveCursorDown();
                        break;
                    case InputAction.MoveUp:
                        MoveCursorUp();
                        break;
                    case InputAction.MoveLeft:
                        MoveCursorLeft();
                        break;
                    case InputAction.MoveRight:
                        MoveCursorRight();
                        break;
                    case InputAction.Select:
                        SelectOption();
                        break;
                    case InputAction.Cancel:
                        BackButtonEvent.Invoke();
                        break;
                }

                _delayOn = true;
            }
        }

        private void MoveCursorLeft()
        {
            _options[_cursorPosition].MoveCursorLeft();
        }

        private void MoveCursorRight()
        {
            _options[_cursorPosition].MoveCursorRight();
        }

        private void MoveCursorDown()
        {
            _cursorPosition = _cursorPosition == _options.Length - 1 ? 0 : ++_cursorPosition;
            UpdateCursor();
        }

        private void MoveCursorUp()
        {
            _cursorPosition = _cursorPosition == 0 ? _options.Length - 1 : --_cursorPosition;
            UpdateCursor();
        }

        private void SelectOption()
        {
            _options[_cursorPosition].Select();
        }

        protected override void UpdateCursor()
        {
            for (int i = 0; i < _options.Length; i++)
            {
                _options[i].ShowCursor(_cursorPosition == i);
            }
        }
    }
}
