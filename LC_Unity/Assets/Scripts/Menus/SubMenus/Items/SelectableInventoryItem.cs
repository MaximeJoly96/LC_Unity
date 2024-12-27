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
                    break;
                case ItemCategory.Weapon:
                    _icon.sprite = FindObjectOfType<WeaponsWrapper>().GetSpriteForWeapon(item.ItemData.Icon);
                    break;
            }
        }
    }
}
