using Core.Model;

namespace Inventory
{
    public class KeyItem : BaseItem
    {
        public ItemUsability Usability { get; protected set; }

        public KeyItem(ElementIdentifier identifier, int icon, int price, ItemCategory category, ItemUsability usability) : 
            base(identifier, icon, price, category)
        {
            Usability = usability;
        }
    }
}
