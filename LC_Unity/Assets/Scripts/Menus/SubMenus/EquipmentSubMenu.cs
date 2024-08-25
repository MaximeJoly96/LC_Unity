﻿using Core;
using TMPro;
using UnityEngine;
using Menus.SubMenus.Items;
using Menus.SubMenus.Status;
using Inputs;
using log4net.Util;
using Language;
using static UnityEditor.Progress;
using Inventory;
using System.Collections.Generic;
using Party;
using System.Linq;

namespace Menus.SubMenus
{
    public class EquipmentSubMenu : SubMenu
    {
        private const float SELECTION_DELAY = 0.2f;

        private int _cursorPosition;
        private float _delay;

        [SerializeField]
        private TMP_Text _characterName;
        [SerializeField]
        private SelectableItemsList _itemsList;
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

        public override void Open()
        {
            _cursorPosition = 0;
            _delay = 0.0f;

            Init();

            _itemsList.ItemHovered.RemoveAllListeners();
            _itemsList.ItemHovered.AddListener(UpdateItemDescription);

            StartCoroutine(DoOpen());
            GlobalStateMachine.Instance.UpdateState(GlobalStateMachine.State.InMenuEquipmentTab);

            UpdateCursor();
            UpdateCurrentItemDetails();
            UpdateItemDescription(null);
        }

        public override void Close()
        {
            StartCoroutine(DoClose());
        }

        protected override void FinishedClosing()
        {
            GlobalStateMachine.Instance.UpdateState(GlobalStateMachine.State.SelectingCharacterPreview);
        }

        private void UpdateItemDescription(SelectableItem item)
        {
            if(item != null)
            {
                _hoveredItemName.text = Localizer.Instance.GetString(item.Item.ItemData.Name);
                _hoveredItemDescription.text = item.Item.ItemData.DetailedDescription();
            }
            else
            {
                _hoveredItemName.text = "";
                _hoveredItemDescription.text = "";
            }
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

        protected override void HandleInputs(InputAction input)
        {
            if(!_busy && GlobalStateMachine.Instance.CurrentState == GlobalStateMachine.State.InMenuEquipmentTab)
            {
                switch(input)
                {
                    case InputAction.Select:
                        Select();
                        break;
                    case InputAction.Cancel:
                        Close();
                        break;
                    case InputAction.MoveUp:
                        MoveUp();
                        break;
                    case InputAction.MoveDown:
                        MoveDown();
                        break;
                }

                _busy = true;
            }
        }

        private void UpdateCursor()
        {
            _equipment.UpdateCursor(_cursorPosition);
        }

        private void MoveUp()
        {
            _cursorPosition = _cursorPosition == 0 ? 4 : --_cursorPosition;
            UpdateCursor();
            UpdateCurrentItemDetails();
        }

        private void MoveDown()
        {
            _cursorPosition = _cursorPosition >= 4 ? 0 : ++_cursorPosition;
            UpdateCursor();
            UpdateCurrentItemDetails();
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
                    _itemsList.Init(itemsToList);
            }
            else
                GlobalStateMachine.Instance.UpdateState(GlobalStateMachine.State.InMenuEquipmentTab);
        }

        private void UpdateCurrentItemDetails()
        {
            BaseItem currentItem = null;
            ItemsWrapper wrapper = GameObject.FindObjectOfType<ItemsWrapper>();

            switch (_cursorPosition)
            {
                case 0:
                    currentItem = wrapper.GetItemFromId(_fedCharacter.RightHand.ItemId);
                    break;
                case 1:
                    currentItem = wrapper.GetItemFromId(_fedCharacter.LeftHand.ItemId);
                    break;
                case 2:
                    currentItem = wrapper.GetItemFromId(_fedCharacter.Head.ItemId);
                    break;
                case 3:
                    currentItem = wrapper.GetItemFromId(_fedCharacter.Body.ItemId);
                    break;
                case 4:
                    currentItem = wrapper.GetItemFromId(_fedCharacter.Accessory.ItemId);
                    break;
            }

            if(currentItem != null)
            {
                _currentItemName.text = Localizer.Instance.GetString(currentItem.Name);
                _currentItemDescription.text = currentItem.DetailedDescription();
            }
            else
            {
                _currentItemName.text = "";
                _currentItemDescription.text = "";
            }
        }
    }
}
