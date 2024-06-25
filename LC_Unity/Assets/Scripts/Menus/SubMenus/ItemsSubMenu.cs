using Core;
using UnityEngine;
using Menus.SubMenus.Items;
using Inputs;

namespace Menus.SubMenus
{
    public class ItemsSubMenu : SubMenu
    {
        private const float SELECTION_DELAY = 0.2f;

        [SerializeField]
        private ItemsCategoryMenu[] _categories;

        private int _cursorPosition;
        private float _delay;

        public override void Open()
        {
            _cursorPosition = 0;
            _delay = 0.0f;

            PlaceCursor();
            StartCoroutine(DoOpen());

            GlobalStateMachine.Instance.UpdateState(GlobalStateMachine.State.InMenuItemsTab);
        }

        public override void Close()
        {
            StartCoroutine(DoClose());
        }

        protected override void FinishedClosing()
        {
            GlobalStateMachine.Instance.UpdateState(GlobalStateMachine.State.InMenu);
        }

        private void PlaceCursor()
        {
            for(int i = 0; i < _categories.Length; i++)
            {
                _categories[i].ShowCursor(_cursorPosition == i);
            }
        }

        protected override void HandleInputs(InputAction input)
        {
            if(!_busy && GlobalStateMachine.Instance.CurrentState == GlobalStateMachine.State.InMenuItemsTab)
            {
                switch(input)
                {
                    case InputAction.MoveLeft:
                        MoveCursorLeft();
                        break;
                    case InputAction.MoveRight:
                        MoveCursorRight();
                        break;
                    case InputAction.MoveDown:
                        break;
                    case InputAction.MoveUp:
                        break;
                    case InputAction.Select:
                        break;
                    case InputAction.Cancel:
                        Close();
                        break;
                }

                _busy = true;
            }
        }

        protected void MoveCursorLeft()
        {
            _cursorPosition = _cursorPosition == 0 ? _categories.Length - 1 : --_cursorPosition;
            PlaceCursor();
        }

        protected void MoveCursorRight()
        {
            _cursorPosition = _cursorPosition == _categories.Length - 1 ? 0 : ++_cursorPosition;
            PlaceCursor();
        }

        private void Update()
        {
            if (_busy)
            {
                _delay += Time.deltaTime;

                if (_delay >= SELECTION_DELAY)
                {
                    _delay = 0.0f;
                    _busy = false;
                }
            }
        }
    }
}
