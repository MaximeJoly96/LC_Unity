namespace Inventory
{
    public class Consumable : BaseItem
    {
        public int Priority { get; protected set; }
        public ItemUsability Usability { get; protected set; }
        public int Animation { get; protected set; }

        public Consumable(int id, string name, string description, int icon, int price, ItemUsability usability, int priority, int animation) :
            base(id, name, description, icon, price)
        {
            Priority = priority;
            Usability = usability;
            Animation = animation;
        }
    }
}
