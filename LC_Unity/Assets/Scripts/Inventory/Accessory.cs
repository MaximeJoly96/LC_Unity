using Core.Model;

namespace Inventory
{
    public class Accessory : EquipmentItem
    {
        public Accessory(ElementIdentifier identifier, int icon, int price, ItemCategory category, int enchantmentSlots) :
            base(identifier, icon, price, category, enchantmentSlots)
        {

        }
    }
}
