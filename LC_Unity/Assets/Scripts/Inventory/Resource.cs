namespace Inventory
{
    public class Resource : BaseItem
    {
        public ItemUsability Usability { get { return ItemUsability.Never; } }

        public Resource(int id, string name, string description, int icon, int price, ItemCategory category) : 
            base(id, name, description, icon, price, category) { }
    }
}
