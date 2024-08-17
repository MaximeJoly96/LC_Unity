using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Inventory;
using Party;
using System.Linq;

namespace Shop
{
    public class ItemDetails : MonoBehaviour
    {
        [SerializeField]
        private TMP_Text _itemName;
        [SerializeField]
        private Image _icon;
        [SerializeField]
        private TMP_Text _itemType;
        [SerializeField]
        private TMP_Text _inStock;
        [SerializeField]
        private CanvasGroup _canvasGroup;

        [SerializeField]
        private TMP_Text _itemDescription;

        public void Feed(BaseItem item)
        {
            _itemName.text = item.Name;

            if (item.Category == ItemCategory.Weapon)
            {
                Weapon weapon = item as Weapon;
                _itemType.text = weapon.Type.ToString();
            }
            else if (item.Category == ItemCategory.Armour)
            {
                Armour armour = item as Armour;
                _itemType.text = armour.Type.ToString();
            }
            else
                _itemType.text = item.Category.ToString();

            InventoryItem inventoryItem = PartyManager.Instance.Inventory.FirstOrDefault(i => i.ItemData.Id == item.Id);
            if (inventoryItem != null)
                _inStock.text = "Possessed: " + inventoryItem.InPossession.ToString();
            else
                _inStock.text = "Possessed: 0";
        }

        public void Show(bool show)
        {
            _canvasGroup.alpha = show ? 1.0f : 0.0f;
        }
    }
}
