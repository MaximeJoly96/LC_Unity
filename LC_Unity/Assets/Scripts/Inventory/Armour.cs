using Core.Model;

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

        public Armour(ElementIdentifier identifier, int icon, int price, ItemCategory category, int enchantmentSlots, ArmourType type) : 
            base(identifier, icon, price, category, enchantmentSlots)
        {
            Type = type;
        }
    }
}
