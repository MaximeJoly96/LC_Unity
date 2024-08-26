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

            BaseItem rightHandItem = itemsWrapper.GetItemFromId(character.RightHand.ItemId);
            BaseItem leftHandItem = itemsWrapper.GetItemFromId(character.LeftHand.ItemId);
            BaseItem headItem = itemsWrapper.GetItemFromId(character.Head.ItemId);
            BaseItem bodyItem = itemsWrapper.GetItemFromId(character.Body.ItemId);
            BaseItem accessoryItem = itemsWrapper.GetItemFromId(character.Accessory.ItemId);

            _rightHandDisplay.Feed(rightHandItem != null ? wrapper.GetSpriteForWeapon(rightHandItem.Icon) : null, character.RightHand.Name);
            _leftHandDisplay.Feed(null, character.LeftHand.Name);
            _headDisplay.Feed(null, character.Head.Name);
            _bodyDisplay.Feed(null, character.Body.Name);
            _accessoryDisplay.Feed(null, character.Accessory.Name);
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
