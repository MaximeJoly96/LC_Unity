namespace Inventory
{
    public class EquipmentItem : BaseItem
    {
        public int EnchantmentSlots { get; protected set; }

        public EquipmentItem(int id, string name, string description, int icon, int price, ItemCategory category, int enchantmentSlots) : 
            base(id, name, description, icon, price, category)
        {
            EnchantmentSlots = enchantmentSlots;
        }
    }
}
