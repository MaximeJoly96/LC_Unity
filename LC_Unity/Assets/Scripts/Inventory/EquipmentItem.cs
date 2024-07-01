namespace Inventory
{
    public class EquipmentItem : BaseItem
    {
        public int EnchantmentSlots { get; protected set; }

        public EquipmentItem(int id, string name, string description, int icon, int price, int enchantmentSlots) : base(id, name, description, icon, price)
        {
            EnchantmentSlots = enchantmentSlots;
        }
    }
}
