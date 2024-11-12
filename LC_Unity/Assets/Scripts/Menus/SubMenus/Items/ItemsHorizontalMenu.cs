using Core;
using Inventory;
using UI;

namespace Menus.SubMenus.Items
{
    public class ItemsHorizontalMenu : HorizontalMenu
    {
        public ItemCategory SelectedCategory { get { return (_buttons[CursorPosition] as ItemsHorizontalMenuButton).Category; } }

        protected override bool CanReceiveInput()
        {
            return GlobalStateMachine.Instance.CurrentState == GlobalStateMachine.State.InMenuItemsTab;
        }
    }
}
