﻿using Core;
using TMPro;
using UnityEngine;
using Menus.SubMenus.Items;
using Menus.SubMenus.Status;
using Language;
using Inventory;
using System.Collections.Generic;
using Party;
using System.Linq;
using Actors;
using Utils;

namespace Menus.SubMenus
{
    public class EquipmentSubMenu : SubMenu
    {
        private int _cursorPosition;
        private SelectableInventoryItem _hoveredItem;
        private BaseItem _currentItem;

        [SerializeField]
        private TMP_Text _characterName;
        [SerializeField]
        private SelectableInventoryItemsList _itemsList;
        [SerializeField]
        private StatsPanel _stats;
        [SerializeField]
        private EquipmentPanel _equipment;
        [SerializeField]
        private TMP_Text _currentItemName;
        [SerializeField]
        private TMP_Text _currentItemDescription;
        [SerializeField]
        private TMP_Text _hoveredItemName;
        [SerializeField]
        private TMP_Text _hoveredItemDescription;

        protected override void BindInputs()
        {
            _inputReceiver.OnSelect.AddListener(() =>
            {
                if(CanReceiveInput())
                {
                    Select();
                }
            });

            _inputReceiver.OnCancel.AddListener(() =>
            {
                if (CanReceiveInput())
                {
                    CommonSounds.ActionCancelled();
                    Cancel();
                }
            });

            _inputReceiver.OnMoveDown.AddListener(() =>
            {
                if (CanReceiveInput())
                {
                    CommonSounds.CursorMoved();
                    MoveDown();
                }
            });

            _inputReceiver.OnMoveUp.AddListener(() =>
            {
                if (CanReceiveInput())
                {
                    CommonSounds.CursorMoved();
                    MoveUp();
                }
            });
        }

        protected override bool CanReceiveInput()
        {
            return !_busy && (GlobalStateMachine.Instance.CurrentState == GlobalStateMachine.State.InMenuEquipmentTab ||
                              GlobalStateMachine.Instance.CurrentState == GlobalStateMachine.State.ChangingEquipment);
        }

        public override void Open()
        {
            _cursorPosition = 0;

            Init();

            _itemsList.ItemSelected.RemoveAllListeners();
            _itemsList.ItemSelected.AddListener(UpdateItemDescription);

            StartCoroutine(DoOpen());
            GlobalStateMachine.Instance.UpdateState(GlobalStateMachine.State.InMenuEquipmentTab);

            UpdateCursor();
            UpdateCurrentItemDetails();
            UpdateItemDescription();
            ClearHoveredItem();
        }

        public override void Close()
        {
            StartCoroutine(DoClose());
        }

        protected override void FinishedClosing()
        {
            GlobalStateMachine.Instance.UpdateState(GlobalStateMachine.State.SelectingCharacterPreview);
        }

        private void UpdateItemDescription()
        {
            SelectableInventoryItem item = (_itemsList.SelectedItem as SelectableInventoryItem);
            if (item != null)
            {
                _hoveredItem = (_itemsList.SelectedItem as SelectableInventoryItem);
                _hoveredItemDescription.text = item.Item.ItemData.DetailedDescription();
                _hoveredItemName.text = Localizer.Instance.GetString(item.Item.ItemData.Name);
            }
            else
                ClearHoveredItem();
        }

        private void Init()
        {
            if(_fedCharacter != null)
            {
                _characterName.text = _fedCharacter.Name;
                _stats.Feed(_fedCharacter);
                _equipment.Feed(_fedCharacter);
            }
        }

        private void UpdateCursor()
        {
            _equipment.UpdateCursor(_cursorPosition);
        }

        private void MoveUp()
        {
            switch(GlobalStateMachine.Instance.CurrentState)
            {
                case GlobalStateMachine.State.InMenuEquipmentTab:
                    _cursorPosition = _cursorPosition == 0 ? 4 : --_cursorPosition;
                    UpdateCursor();
                    UpdateCurrentItemDetails();
                    break;
                case GlobalStateMachine.State.ChangingEquipment:

                    break;
            }
        }

        private void MoveDown()
        {
            switch (GlobalStateMachine.Instance.CurrentState)
            {
                case GlobalStateMachine.State.InMenuEquipmentTab:
                    _cursorPosition = _cursorPosition >= 4 ? 0 : ++_cursorPosition;
                    UpdateCursor();
                    UpdateCurrentItemDetails();
                    break;
                case GlobalStateMachine.State.ChangingEquipment:
                    
                    break;
            }
        }

        private void Select()
        {
            if(GlobalStateMachine.Instance.CurrentState == GlobalStateMachine.State.InMenuEquipmentTab)
            {
                GlobalStateMachine.Instance.UpdateState(GlobalStateMachine.State.ChangingEquipment);

                IEnumerable<InventoryItem> itemsToList = null;

                switch(_cursorPosition)
                {
                    case 0:
                        itemsToList = PartyManager.Instance.Inventory.Where(i => i.ItemData.Category == ItemCategory.Weapon);
                        break;
                    case 1:
                        itemsToList = PartyManager.Instance.Inventory.Where(i => i.ItemData.Category == ItemCategory.Armour &&
                                                                            (i.ItemData as Armour).Type == ArmourType.Shield);
                        break;
                    case 2:
                        itemsToList = PartyManager.Instance.Inventory.Where(i => i.ItemData.Category == ItemCategory.Armour &&
                                                                            (i.ItemData as Armour).Type == ArmourType.Head);
                        break;
                    case 3:
                        itemsToList = PartyManager.Instance.Inventory.Where(i => i.ItemData.Category == ItemCategory.Armour &&
                                                                            (i.ItemData as Armour).Type == ArmourType.Body);
                        break;
                    case 4:
                        itemsToList = PartyManager.Instance.Inventory.Where(i => i.ItemData.Category == ItemCategory.Accessory);
                        break;
                }

                if(itemsToList != null)
                {
                    _itemsList.Init();
                    _itemsList.ShowContent(itemsToList);
                }
            }
            else
            {
                if (_hoveredItem != null)
                {
                    ModifyEquipment(_fedCharacter, _hoveredItem.Item.ItemData, _cursorPosition);
                    _itemsList.Clear();
                    ClearHoveredItem();

                    UpdateCursor();
                    UpdateCurrentItemDetails();
                    UpdateItemDescription();
                    GlobalStateMachine.Instance.UpdateState(GlobalStateMachine.State.InMenuEquipmentTab);
                }
                else
                    CommonSounds.Error();
            }
                
        }

        private void Cancel()
        {
            switch(GlobalStateMachine.Instance.CurrentState)
            {
                case GlobalStateMachine.State.InMenuEquipmentTab:
                    Close();
                    break;
                case GlobalStateMachine.State.ChangingEquipment:
                    _itemsList.Clear();
                    ClearHoveredItem();
                    GlobalStateMachine.Instance.UpdateState(GlobalStateMachine.State.InMenuEquipmentTab);
                    break;
            }
        }

        private void UpdateCurrentItemDetails()
        {
            ItemsWrapper wrapper = GameObject.FindObjectOfType<ItemsWrapper>();

            switch (_cursorPosition)
            {
                case 0:
                    _currentItem = wrapper.GetItemFromId(_fedCharacter.Equipment.RightHand.ItemId);
                    break;
                case 1:
                    _currentItem = wrapper.GetItemFromId(_fedCharacter.Equipment.LeftHand.ItemId);
                    break;
                case 2:
                    _currentItem = wrapper.GetItemFromId(_fedCharacter.Equipment.Head.ItemId);
                    break;
                case 3:
                    _currentItem = wrapper.GetItemFromId(_fedCharacter.Equipment.Body.ItemId);
                    break;
                case 4:
                    _currentItem = wrapper.GetItemFromId(_fedCharacter.Equipment.Accessory.ItemId);
                    break;
            }

            if(_currentItem != null)
            {
                _currentItemName.text = Localizer.Instance.GetString(_currentItem.Name);
                _currentItemDescription.text = _currentItem.DetailedDescription();
            }
            else
            {
                _currentItemName.text = "";
                _currentItemDescription.text = "";
                _currentItem = null;
            }
        }

        private void ClearHoveredItem()
        {
            _hoveredItemName.text = "";
            _hoveredItemDescription.text = "";
            _hoveredItem = null;
        }

        private void ModifyEquipment(Character character, BaseItem newItem, int cursorPosition)
        {
            switch(cursorPosition)
            {
                case 0:
                    character.Equipment.RightHand.ItemId = newItem.Id;
                    break;
                case 1:
                    character.Equipment.LeftHand.ItemId = newItem.Id;
                    break;
                case 2:
                    character.Equipment.Head.ItemId = newItem.Id;
                    break;
                case 3:
                    character.Equipment.Body.ItemId = newItem.Id;
                    break;
                case 4:
                    character.Equipment.Accessory.ItemId = newItem.Id;
                    break;
            }

            Init();
        }
    }
}
