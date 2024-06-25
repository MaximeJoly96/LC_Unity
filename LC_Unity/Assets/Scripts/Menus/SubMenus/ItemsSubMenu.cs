using Core;
using UnityEngine;
using Menus.SubMenus.Items;
using Inputs;
using TMPro;

namespace Menus.SubMenus
{
    public class ItemsSubMenu : SubMenu
    {
        private const float SELECTION_DELAY = 0.2f;

        [SerializeField]
        private ItemsCategoryMenu[] _categories;
        [SerializeField]
        private SelectableItemsList _itemsList;
        [SerializeField]
        private TMP_Text _currentItemDescription;

        private int _cursorPosition;
        private float _delay;

        public override void Open()
        {
            _cursorPosition = 0;
            _delay = 0.0f;

            _itemsList.ItemHovered.RemoveAllListeners();
            _itemsList.ItemHovered.AddListener(UpdateItemDescription);

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

            _itemsList.Init(_categories[_cursorPosition].Category);
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
                        MoveCursorDown();
                        break;
                    case InputAction.MoveUp:
                        MoveCursorUp();
                        break;
                    case InputAction.Select:
                        Select();
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

        private void MoveCursorDown()
        {
            _itemsList.MoveCursorDown();
        }

        private void MoveCursorUp()
        {
            _itemsList.MoveCursorUp();
        }

        private void Select()
        {
            _itemsList.Select();
        }

        private void UpdateItemDescription(SelectableItem item)
        {
            _currentItemDescription.text = item.Item.Description;
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
