using UnityEngine;
using Inputs;
using UnityEngine.Events;
using System.Collections.Generic;
using Engine.MusicAndSounds;
using MusicAndSounds;
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

        protected override void ReceiveInput(InputAction input)
        {
            if (!_delayOn)
            {
                switch (input)
                {
                    case InputAction.MoveDown:
                        CommonSounds.CursorMoved();
                        MoveCursorDown();
                        break;
                    case InputAction.MoveUp:
                        CommonSounds.CursorMoved();
                        MoveCursorUp();
                        break;
                    case InputAction.Select:
                        CommonSounds.OptionSelected();
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
