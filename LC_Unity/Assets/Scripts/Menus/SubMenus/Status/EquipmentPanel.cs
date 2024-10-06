using UnityEngine;
using Actors;
using Utils;
using Inventory;

namespace Menus.SubMenus.Status
{
    public class EquipmentPanel : StatusSubPanel
    {
        [SerializeField]
        private EquipmentDisplay _rightHandDisplay;
        [SerializeField]
        private EquipmentDisplay _leftHandDisplay;
        [SerializeField]
        private EquipmentDisplay _headDisplay;
        [SerializeField]
        private EquipmentDisplay _bodyDisplay;
        [SerializeField]
        private EquipmentDisplay _accessoryDisplay;

        public override void Feed(Character character)
        {
            WeaponsWrapper wrapper = FindObjectOfType<WeaponsWrapper>();
            ItemsWrapper itemsWrapper = FindObjectOfType<ItemsWrapper>();

            BaseItem rightHandItem = itemsWrapper.GetItemFromId(character.Equipment.RightHand.ItemId);
            BaseItem leftHandItem = itemsWrapper.GetItemFromId(character.Equipment.LeftHand.ItemId);
            BaseItem headItem = itemsWrapper.GetItemFromId(character.Equipment.Head.ItemId);
            BaseItem bodyItem = itemsWrapper.GetItemFromId(character.Equipment.Body.ItemId);
            BaseItem accessoryItem = itemsWrapper.GetItemFromId(character.Equipment.Accessory.ItemId);

            _rightHandDisplay.Feed(rightHandItem != null ? wrapper.GetSpriteForWeapon(rightHandItem.Icon) : null, character.Equipment.RightHand.Name);
            _leftHandDisplay.Feed(null, character.Equipment.LeftHand.Name);
            _headDisplay.Feed(null, character.Equipment.Head.Name);
            _bodyDisplay.Feed(null, character.Equipment.Body.Name);
            _accessoryDisplay.Feed(null, character.Equipment.Accessory.Name);
        }

        public void UpdateCursor(int cursorPosition)
        {
            _rightHandDisplay.ShowCursor(cursorPosition == 0);
            _leftHandDisplay.ShowCursor(cursorPosition == 1);
            _headDisplay.ShowCursor(cursorPosition == 2);
            _bodyDisplay.ShowCursor(cursorPosition == 3);
            _accessoryDisplay.ShowCursor(cursorPosition == 4);
        }
    }
}
