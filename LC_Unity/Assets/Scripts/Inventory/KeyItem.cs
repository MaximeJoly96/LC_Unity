namespace Inventory
{
    public class KeyItem : BaseItem
    {
        public ItemUsability Usability { get; protected set; }

        public KeyItem(int id, string name, string description, int icon, int price, ItemCategory category, ItemUsability usability) : 
            base(id, name, description, icon, price, category)
        {
            Usability = usability;
        }
    }
}
