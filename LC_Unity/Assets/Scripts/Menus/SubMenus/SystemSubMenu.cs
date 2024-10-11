using Core;
using UnityEngine;
using Menus.SubMenus.System;
using Utils;

namespace Menus.SubMenus
{
    public class SystemSubMenu : SubMenu 
    {
        [SerializeField]
        private SystemSubMenuItem[] _subMenuItems;

        private int _cursorPosition;

        protected override bool CanReceiveInput()
        {
            return !_busy && GlobalStateMachine.Instance.CurrentState == GlobalStateMachine.State.InMenuSystemTab;
        }

        protected override void BindInputs()
        {
            _inputReceiver.OnSelect.AddListener(() =>
            {
                if(CanReceiveInput())
                {
                    CommonSounds.OptionSelected();
                    SelectItem();
                }
            });

            _inputReceiver.OnCancel.AddListener(() =>
            {
                if (CanReceiveInput())
                {
                    CommonSounds.ActionCancelled();
                    Close();
                }
            });

            _inputReceiver.OnMoveDown.AddListener(() =>
            {
                if (CanReceiveInput())
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
        }

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
