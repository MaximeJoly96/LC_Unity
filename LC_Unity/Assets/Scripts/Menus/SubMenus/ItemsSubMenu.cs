using Core;
using UnityEngine;
using Menus.SubMenus.Items;
using Utils;
using System.Collections;
using Party;
using Inventory;

namespace Menus.SubMenus
{
    public class ItemsSubMenu : SubMenu
    {
        [SerializeField]
        private ItemsHorizontalMenu _horizontalMenu;
        [SerializeField]
        private SelectableInventoryItemsList _itemsList;
        [SerializeField]
        private ItemDetails _itemDetails;

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
        }

        public override void Open()
        {
            _horizontalMenu.Init();

            _itemsList.SelectionChanged.RemoveAllListeners();
            _itemsList.SelectionChanged.AddListener(UpdateItemDescription);
            _itemsList.SelectionCancelled.RemoveAllListeners();
            _itemsList.SelectionCancelled.AddListener(ClearSelectedList);
            _itemsList.ItemSelected.RemoveAllListeners();
            _itemsList.ItemSelected.AddListener(SelectItem);

            PartyManager.Instance.InventoryChanged.AddListener(InventoryUpdated);

            StartCoroutine(DoOpen());
            ClearSelectedList();
        }

        public override void Close()
        {
            PartyManager.Instance.InventoryChanged.RemoveListener(InventoryUpdated);
            MainMenuRefreshRequested.Invoke();

            StartCoroutine(DoClose());
        }

        protected override void FinishedClosing()
        {
            GlobalStateMachine.Instance.UpdateState(GlobalStateMachine.State.InMenu);
        }

        private void Select()
        {
            StartCoroutine(DoSelect());
        }

        private IEnumerator DoSelect()
        {
            _itemsList.Init();
            _itemsList.ShowContent(_horizontalMenu.SelectedCategory);
            UpdateItemDescription();

            yield return new WaitForSeconds(0.05f);
            GlobalStateMachine.Instance.UpdateState(GlobalStateMachine.State.BrowsingInventory);
        }

        private void UpdateItemDescription()
        {
            _itemDetails.Feed((_itemsList.SelectedItem as SelectableInventoryItem).Item);
        }

        private void ClearSelectedList()
        {
            _itemDetails.Clear();
            _itemsList.Clear();
            GlobalStateMachine.Instance.UpdateState(GlobalStateMachine.State.InMenuItemsTab);
        }

        private void SelectItem()
        {
            InventoryItem inventoryItem = (_itemsList.SelectedItem as SelectableInventoryItem).Item;
            if (inventoryItem.ItemData.Category == ItemCategory.Consumable &&
               ((inventoryItem.ItemData as Consumable).Usability == ItemUsability.Always ||
               (inventoryItem.ItemData as Consumable).Usability == ItemUsability.MenuOnly))
            {
                CommonSounds.OptionSelected();
                FindObjectOfType<MainMenuController>().OpenCharacterTargetingWithItem(_itemsList.SelectedItem as SelectableInventoryItem);
            }
            else
                CommonSounds.Error();
        }

        private void InventoryUpdated()
        {
            _itemsList.ShowContent(_horizontalMenu.SelectedCategory);
            UpdateItemDescription();
        }
    }
}
