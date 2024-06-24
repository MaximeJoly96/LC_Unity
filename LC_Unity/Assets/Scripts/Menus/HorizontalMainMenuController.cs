using UnityEngine;
using Core;
using Inputs;
using Actors;

namespace Menus
{
    public class HorizontalMainMenuController : MonoBehaviour
    {
        private const float DELAY_BETWEEN_ACTIONS = 0.2f; // seconds

        [SerializeField]
        private SubMenuButton[] _subMenuButtons;

        private InputController _inputController;
        private int _cursorPosition;
        private float _selectionDelay;
        private bool _busy;

        private void Start()
        {
            _inputController = FindObjectOfType<InputController>();
            _inputController.ButtonClicked.AddListener(HandleInputs);

            Init();
        }

        public void Init()
        {
            _cursorPosition = 0;
            _selectionDelay = 0.0f;
            _busy = false;

            UpdateCursorPosition();
        }

        private void HandleInputs(InputAction input)
        {
            if(GlobalStateMachine.Instance.CurrentState == GlobalStateMachine.State.InMenu && !_busy)
            {
                switch(input)
                {
                    case InputAction.MoveLeft:
                        MoveCursorLeft();
                        break;
                    case InputAction.MoveRight:
                        MoveCursorRight();
                        break;
                    case InputAction.Select:
                        SelectSubMenu();
                        break;
                }
            }
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

            _busy = true;
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

        private void Update()
        {
            if(_busy)
            {
                _selectionDelay += Time.deltaTime;

                if (_selectionDelay >= DELAY_BETWEEN_ACTIONS)
                {
                    _selectionDelay = 0.0f;
                    _busy = false;
                }
            }
        }
    }
}
