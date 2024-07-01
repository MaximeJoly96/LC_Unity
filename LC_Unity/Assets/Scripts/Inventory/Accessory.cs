namespace Inventory
{
    public class Accessory : EquipmentItem
    {
        public Accessory(int id, string name, string description, int icon, int price, int enchantmentSlots) :
            base(id, name, description, icon, price, enchantmentSlots)
        {

        }
    }
}
