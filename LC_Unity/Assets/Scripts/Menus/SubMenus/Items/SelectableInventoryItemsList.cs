using Inventory;
using Party;
using System.Collections.Generic;
using System.Linq;
using UI;
using Core;

namespace Menus.SubMenus.Items
{
    public class SelectableInventoryItemsList : SelectableList
    {
        public void ShowContent(ItemCategory category)
        {
            ShowContent(PartyManager.Instance.Inventory.Where(i => i.ItemData.Category == category));
        }

        public void ShowContent(IEnumerable<InventoryItem> items)
        {
            Clear();

            for (int i = 0; i < items.Count(); i++)
            {
                SelectableInventoryItem item = AddItem() as SelectableInventoryItem;
                item.Feed(items.ElementAt(i));
                item.ShowCursor(false);
            }

            PlaceCursor();
        }

        protected override bool CanReceiveInputs()
        {
            return GlobalStateMachine.Instance.CurrentState == GlobalStateMachine.State.BrowsingInventory;
        }
    }
}
