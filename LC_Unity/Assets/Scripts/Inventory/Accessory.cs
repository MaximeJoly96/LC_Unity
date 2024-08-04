namespace Inventory
{
    public class Accessory : EquipmentItem
    {
        public Accessory(int id, string name, string description, int icon, int price, ItemCategory category, int enchantmentSlots) :
            base(id, name, description, icon, price, category, enchantmentSlots)
        {

        }
    }
}
