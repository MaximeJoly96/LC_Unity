namespace Inventory
{
    public class Resource : BaseItem
    {
        public ItemUsability Usability { get { return ItemUsability.Never; } }

        public Resource(int id, string name, string description, int icon, int price) : base(id, name, description, icon, price) { }
    }
}
