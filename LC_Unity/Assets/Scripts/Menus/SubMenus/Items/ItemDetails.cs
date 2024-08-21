using UnityEngine;
using TMPro;
using Inventory;
using UnityEngine.UI;
using Language;
using Utils;

namespace Menus.SubMenus.Items
{
    public class ItemDetails : MonoBehaviour
    {
        [SerializeField]
        private TMP_Text _itemName;
        [SerializeField]
        private TMP_Text _description;
        [SerializeField]
        private Image _icon;
        [SerializeField]
        private TMP_Text _inStock;

        private InventoryItem _inventoryItem;

        public void Feed(InventoryItem item)
        {
            _inventoryItem = item;
            _itemName.text = Localizer.Instance.GetString(_inventoryItem.ItemData.Name);
            _inStock.text = Localizer.Instance.GetString("inStock") + " " + item.InPossession;

            _description.text = item.ItemData.DetailedDescription();

            switch(item.ItemData.Category)
            {
                case ItemCategory.Consumable:
                    _icon.sprite = FindObjectOfType<ConsumablesWrapper>().GetSpriteForConsumable(item.ItemData.Icon);
                    break;
            }
        }

        public void Clear()
        {
            _inventoryItem = null;
            _itemName.text = "";
            _inStock.text = "";
            _description.text = "";
            _icon.sprite = null;
        }
    }
}
