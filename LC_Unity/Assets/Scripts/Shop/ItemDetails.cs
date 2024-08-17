using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Inventory;

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
        }
    }
}
