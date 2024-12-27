using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Inventory;
using Language;
using Utils;
using UI;

namespace Menus.SubMenus.Items
{
    public class SelectableInventoryItem : SelectableItem
    {
        [SerializeField]
        private Image _icon;
        [SerializeField]
        private TMP_Text _quantity;

        public InventoryItem Item { get; private set; }

        public void Feed(InventoryItem item)
        {
            Item = item;

            _label.text = Localizer.Instance.GetString(item.ItemData.Name);
            _quantity.text = "x" + item.InPossession;

            switch(item.ItemData.Category)
            {
                case ItemCategory.Consumable:
                    _icon.sprite = FindObjectOfType<ConsumablesWrapper>().GetSpriteForConsumable(item.ItemData.Icon);
                    Disable((item.ItemData as Consumable).Usability == ItemUsability.BattleOnly ||
                            (item.ItemData as Consumable).Usability == ItemUsability.Never);
                    break;
                case ItemCategory.Weapon:
                    _icon.sprite = FindObjectOfType<WeaponsWrapper>().GetSpriteForWeapon(item.ItemData.Icon);
                    break;
            }
        }

        private void Disable(bool disable)
        {
            Color toUse = disable ? Color.grey : Color.white;

            _label.color = toUse;
            _quantity.color = toUse;
        }
    }
}
