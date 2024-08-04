namespace Inventory
{
    public enum ArmourType
    {
        Head,
        Shield,
        Body
    }

    public class Armour : EquipmentItem
    {
        public ArmourType Type { get; protected set; }

        public Armour(int id, string name, string description, int icon, int price, ItemCategory category, int enchantmentSlots, ArmourType type) : 
            base(id, name, description, icon, price, category, enchantmentSlots)
        {
            Type = type;
        }
    }
}
