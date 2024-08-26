using Core;
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
using Actors;

namespace Menus.SubMenus
{
    public class EquipmentSubMenu : SubMenu
    {
        private const float SELECTION_DELAY = 0.2f;

        private int _cursorPosition;
        private float _delay;
        private SelectableItem _hoveredItem;
        private BaseItem _currentItem;

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
                _hoveredItem = item;
            }
            else
            {
                ClearHoveredItem();
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
            if(!_busy && (GlobalStateMachine.Instance.CurrentState == GlobalStateMachine.State.InMenuEquipmentTab ||
                          GlobalStateMachine.Instance.CurrentState == GlobalStateMachine.State.ChangingEquipment))
            {
                switch(input)
                {
                    case InputAction.Select:
                        Select();
                        break;
                    case InputAction.Cancel:
                        Cancel();
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
            switch(GlobalStateMachine.Instance.CurrentState)
            {
                case GlobalStateMachine.State.InMenuEquipmentTab:
                    _cursorPosition = _cursorPosition == 0 ? 4 : --_cursorPosition;
                    UpdateCursor();
                    UpdateCurrentItemDetails();
                    break;
                case GlobalStateMachine.State.ChangingEquipment:
                    _itemsList.MoveCursorUp();
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
                    _itemsList.MoveCursorDown();
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
                    _itemsList.Init(itemsToList);
            }
            else
            {
                ModifyEquipment(_fedCharacter, _hoveredItem.Item.ItemData, _cursorPosition);
                _itemsList.Clear();
                ClearHoveredItem();

                UpdateCursor();
                UpdateCurrentItemDetails();
                UpdateItemDescription(null);
                GlobalStateMachine.Instance.UpdateState(GlobalStateMachine.State.InMenuEquipmentTab);
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
                    _currentItem = wrapper.GetItemFromId(_fedCharacter.RightHand.ItemId);
                    break;
                case 1:
                    _currentItem = wrapper.GetItemFromId(_fedCharacter.LeftHand.ItemId);
                    break;
                case 2:
                    _currentItem = wrapper.GetItemFromId(_fedCharacter.Head.ItemId);
                    break;
                case 3:
                    _currentItem = wrapper.GetItemFromId(_fedCharacter.Body.ItemId);
                    break;
                case 4:
                    _currentItem = wrapper.GetItemFromId(_fedCharacter.Accessory.ItemId);
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
                    character.RightHand.ItemId = newItem.Id;
                    break;
                case 1:
                    character.LeftHand.ItemId = newItem.Id;
                    break;
                case 2:
                    character.Head.ItemId = newItem.Id;
                    break;
                case 3:
                    character.Body.ItemId = newItem.Id;
                    break;
                case 4:
                    character.Accessory.ItemId = newItem.Id;
                    break;
            }

            Init();
        }
    }
}
