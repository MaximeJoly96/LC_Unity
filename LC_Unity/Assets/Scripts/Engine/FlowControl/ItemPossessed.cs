using Inventory;
using Party;
using System.Linq;

namespace Engine.FlowControl
{
    public class ItemPossessed : InventoryCondition
    {
        public int MinQuantity { get; set; }

        public override void Run()
        {
            InventoryItem item = PartyManager.Instance.Inventory.FirstOrDefault(i => i.ItemData.Id == ItemId);
            bool result = false;

            if (item != null)
            {
                result = item.InPossession >= MinQuantity;
            }

            DefineSequences(result);
        }
    }
}
