using UnityEngine;
using Actors;

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
            _rightHandDisplay.Feed(null, character.RightHand.Name);
            _leftHandDisplay.Feed(null, character.LeftHand.Name);
            _headDisplay.Feed(null, character.Head.Name);
            _bodyDisplay.Feed(null, character.Body.Name);
            _accessoryDisplay.Feed(null, character.Accessory.Name);
        }
    }
}
