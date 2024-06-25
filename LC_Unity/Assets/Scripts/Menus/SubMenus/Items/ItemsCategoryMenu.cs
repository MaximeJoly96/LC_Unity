using UnityEngine;
using Inventory;

namespace Menus.SubMenus.Items
{
    public class ItemsCategoryMenu : MonoBehaviour
    {
        [SerializeField]
        private ItemCategory _category;

        public ItemCategory Category { get { return _category; } }

        public void ShowCursor(bool show)
        {
            transform.Find("Bg").gameObject.SetActive(show);
            transform.Find("CursorWrapper").gameObject.SetActive(show);
        }
    }
}
