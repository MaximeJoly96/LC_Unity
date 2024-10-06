using Core.Model;

namespace Inventory
{
    public class Resource : BaseItem
    {
        public ItemUsability Usability { get { return ItemUsability.Never; } }

        public Resource(ElementIdentifier identifier, int icon, int price, ItemCategory category) : 
            base(identifier, icon, price, category) { }
    }
}
