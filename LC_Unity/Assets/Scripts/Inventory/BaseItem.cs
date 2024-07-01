namespace Inventory
{
    public enum ItemUsability
    {
        Never,
        Always,
        BattleOnly,
        MenuOnly
    }

    public class BaseItem
    {
        public int Id { get; protected set; }
        public string Name { get; protected set; }
        public string Description { get; protected set; }
        public int Icon { get; protected set; }
        public int Price { get; protected set; }

        public BaseItem(int id, string name, string description, int icon, int price)
        {
            Id = id;
            Name = name;
            Description = description;
            Icon = icon;
            Price = price;
        }
    }
}
