using UnityEngine;
using Core;
using Actors;
using Utils;

namespace Menus
{
    public class HorizontalMainMenuController : MonoBehaviour
    {
        [SerializeField]
        private SubMenuButton[] _subMenuButtons;

        private InputReceiver _inputReceiver;
        private int _cursorPosition;

        private void Start()
        {
            BindInputs();
            Init();
        }

        private void BindInputs()
        {
            _inputReceiver = GetComponent<InputReceiver>();

            _inputReceiver.OnMoveLeft.AddListener(() =>
            {
                if(CanReceiveInput())
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
                    SelectSubMenu();
                }
            });
        }

        private bool CanReceiveInput()
        {
            return GlobalStateMachine.Instance.CurrentState == GlobalStateMachine.State.InMenu;
        }

        public void Init()
        {
            _cursorPosition = 0;

            UpdateCursorPosition();
        }

        private void MoveCursorLeft()
        {
            _cursorPosition = _cursorPosition == 0 ? _subMenuButtons.Length - 1 : --_cursorPosition;
            UpdateCursorPosition();
        }

        private void MoveCursorRight()
        {
            _cursorPosition = _cursorPosition == _subMenuButtons.Length - 1 ? 0 : ++_cursorPosition;
            UpdateCursorPosition();
        }

        private void UpdateCursorPosition()
        {
            for(int i = 0; i < _subMenuButtons.Length; i++)
            {
                _subMenuButtons[i].DisplayCursor(i == _cursorPosition);
            }
        }

        private void SelectSubMenu()
        {
            _subMenuButtons[_cursorPosition].SelectSubMenu();
        }

        public void OpenCharacterTabWithSelectedCharacter(Character character)
        {
            _subMenuButtons[_cursorPosition].FeedCharacterDataToSubMenu(character);
            _subMenuButtons[_cursorPosition].OpenSubMenu();
        }
    }
}
