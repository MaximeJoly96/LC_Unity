using Core;
using UnityEngine;
using Menus.SubMenus.System;
using Inputs;
using Menus.SubMenus.Items;
using Utils;

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
            if(!_busy && GlobalStateMachine.Instance.CurrentState == GlobalStateMachine.State.InMenuSystemTab)
            {
                switch(input)
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
                        SelectItem();
                        break;
                    case InputAction.MoveLeft:
                        CommonSounds.CursorMoved();
                        MoveCursorLeft();
                        break;
                    case InputAction.MoveRight:
                        CommonSounds.CursorMoved();
                        MoveCursorRight();
                        break;
                    case InputAction.Cancel:
                        CommonSounds.ActionCancelled();
                        Close();
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

        private void MoveCursorLeft()
        {
            if (_subMenuItems[_cursorPosition] is SystemSubMenuVolumeEditor ||
                _subMenuItems[_cursorPosition] is SystemSubMenuSelectLanguage)
                _subMenuItems[_cursorPosition].MoveCursorLeft();
        }

        private void MoveCursorRight()
        {
            if (_subMenuItems[_cursorPosition] is SystemSubMenuVolumeEditor ||
                _subMenuItems[_cursorPosition] is SystemSubMenuSelectLanguage)
                _subMenuItems[_cursorPosition].MoveCursorRight();
        }
    }
}
