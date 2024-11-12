using Core;
using UnityEngine;
using Menus.SubMenus.Items;
using Inputs;
using Utils;

namespace Menus.SubMenus
{
    public class ItemsSubMenu : SubMenu
    {
        [SerializeField]
        private ItemsHorizontalMenu _horizontalMenu;
        [SerializeField]
        private SelectableItemsList _itemsList;
        [SerializeField]
        private ItemDetails _itemDetails;

        private int _cursorPosition;

        protected override bool CanReceiveInput()
        {
            return !_busy && GlobalStateMachine.Instance.CurrentState == GlobalStateMachine.State.InMenuItemsTab;
        }

        protected override void BindInputs()
        {
            _inputReceiver.OnSelect.AddListener(() =>
            {
                if(CanReceiveInput())
                {
                    CommonSounds.OptionSelected();
                    Select();
                }
            });

            _inputReceiver.OnCancel.AddListener(() =>
            {
                if(CanReceiveInput())
                {
                    CommonSounds.ActionCancelled();
                    Close();
                }
            });

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
                if(CanReceiveInput())
                {
                    CommonSounds.CursorMoved();
                    MoveCursorUp();
                }
            });
        }

        public override void Open()
        {
            _horizontalMenu.Init();
            _cursorPosition = 0;

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
            _itemDetails.Clear();
            _itemsList.Init(_horizontalMenu.SelectedCategory);
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
            _itemDetails.Feed(item.Item);
        }
    }
}
