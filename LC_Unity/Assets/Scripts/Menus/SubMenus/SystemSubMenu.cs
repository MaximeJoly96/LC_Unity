using Core;
using UnityEngine;
using Menus.SubMenus.System;
using Inputs;
using Menus.SubMenus.Items;

namespace Menus.SubMenus
{
    public class SystemSubMenu : SubMenu 
    {
        private const float SELECTION_DELAY = 0.2f;

        [SerializeField]
        private SystemSubMenuItem[] _subMenuItems;

        private float _selectionDelay;
        private int _cursorPosition;

        public override void Open()
        {
            StartCoroutine(DoOpen());
            GlobalStateMachine.Instance.UpdateState(GlobalStateMachine.State.InMenuSystemTab);
            UpdateCursor();
        }

        public override void Close()
        {
            StartCoroutine(DoClose());
        }

        protected override void FinishedClosing()
        {
            GlobalStateMachine.Instance.UpdateState(GlobalStateMachine.State.InMenu);
        }

        protected override void HandleInputs(InputAction input)
        {
            base.HandleInputs(input);

            if(!_busy && GlobalStateMachine.Instance.CurrentState == GlobalStateMachine.State.InMenuSystemTab)
            {
                switch(input)
                {
                    case InputAction.MoveDown:
                        MoveCursorDown();
                        break;
                    case InputAction.MoveUp:
                        MoveCursorUp();
                        break;
                    case InputAction.Select:
                        SelectItem();
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

        private void MoveCursorUp()
        {
            _cursorPosition = _cursorPosition == 0 ? _subMenuItems.Length - 1 : --_cursorPosition;
            UpdateCursor();
        }

        private void MoveCursorDown()
        {
            _cursorPosition = _cursorPosition == _subMenuItems.Length - 1 ? 0 : ++_cursorPosition;
            UpdateCursor();
        }

        private void UpdateCursor()
        {
            for(int i = 0; i < _subMenuItems.Length; i++)
            {
                _subMenuItems[i].Hover(i ==  _cursorPosition);
            }
        }

        private void SelectItem()
        {
            _subMenuItems[_cursorPosition].Select();
        }
    }
}
