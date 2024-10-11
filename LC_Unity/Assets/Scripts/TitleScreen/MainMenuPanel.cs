using UnityEngine;
using UnityEngine.Events;
using Utils;

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

        protected override void BindInputs()
        {
            _inputReceiver.OnMoveDown.AddListener(() =>
            {
                if(CanReceiveInput())
                {
                    CommonSounds.CursorMoved();
                    MoveCursorDown();
                }
            });

            _inputReceiver.OnMoveUp.AddListener(() =>
            {
                if (CanReceiveInput())
                {
                    CommonSounds.CursorMoved();
                    MoveCursorUp();
                }
            });

            _inputReceiver.OnSelect.AddListener(() =>
            {
                if (CanReceiveInput())
                {
                    CommonSounds.OptionSelected();
                    SelectOption();
                }
            });
        }

        protected override bool CanReceiveInput()
        {
            return true;
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
