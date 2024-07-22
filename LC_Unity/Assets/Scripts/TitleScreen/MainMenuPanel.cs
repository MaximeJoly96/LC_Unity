using UnityEngine;
using Inputs;
using UnityEngine.Events;

namespace TitleScreen
{
    public class MainMenuPanel : TitleScreenPanel
    {
        internal enum MainMenuOptions { NewGame, LoadGame, ChangeOptions, Quit }

        [SerializeField]
        private TitleScreenOption[] _options;

        private UnityEvent<MainMenuOptions> _optionSelected;
        internal UnityEvent<MainMenuOptions> OptionSelected
        {
            get
            {
                if (_optionSelected == null)
                    _optionSelected = new UnityEvent<MainMenuOptions>();

                return _optionSelected;
            }
        }

        protected override void ReceiveInput(InputAction input)
        {
            if (!_delayOn)
            {
                switch (input)
                {
                    case InputAction.MoveDown:
                        MoveCursorDown();
                        break;
                    case InputAction.MoveUp:
                        MoveCursorUp();
                        break;
                    case InputAction.Select:
                        SelectOption();
                        break;
                }

                _delayOn = true;
            }
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
            _lockedChoice = true;

            OptionSelected.Invoke(_options[_cursorPosition].Option);
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
