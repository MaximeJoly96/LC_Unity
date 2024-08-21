using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Inventory;
using Language;

namespace Menus.SubMenus.Items
{
    public class SelectableItem : MonoBehaviour
    {
        [SerializeField]
        private Image _icon;
        [SerializeField]
        private TMP_Text _name;
        [SerializeField]
        private TMP_Text _quantity;
        [SerializeField]
        private Transform _cursor;

        public InventoryItem Item { get; private set; }

        public void Feed(InventoryItem item)
        {
            Item = item;

            _icon.sprite = null;//item.ItemData.Icon;
            _name.text = Localizer.Instance.GetString(item.ItemData.Name);
            _quantity.text = "x" + item.InPossession;
        }

        public void ShowCursor(bool show)
        {
            _cursor.gameObject.SetActive(show);
        }
    }
}
