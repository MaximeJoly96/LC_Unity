using Inventory;
using UI;
using UnityEngine;

namespace Menus.SubMenus.Items
{
    public class ItemsHorizontalMenuButton : HorizontalMenuButton
    {
        [SerializeField]
        private ItemCategory _category;

        public ItemCategory Category { get { return _category; } }
    }
}
