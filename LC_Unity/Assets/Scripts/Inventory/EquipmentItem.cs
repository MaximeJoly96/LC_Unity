using Effects;
using Language;
using Core.Model;

namespace Inventory
{
    public class EquipmentItem : BaseItem
    {
        public int EnchantmentSlots { get; protected set; }
        public ItemStats Stats { get; set; }

        public EquipmentItem(ElementIdentifier identifier, int icon, int price, ItemCategory category, int enchantmentSlots) : 
            base(identifier, icon, price, category)
        {
            EnchantmentSlots = enchantmentSlots;
        }

        public override string DetailedDescription()
        {
            string description = Localizer.Instance.GetString(Description) + "\n";

            if(Stats != null)
                description += Stats.ToString();

            description += Localizer.Instance.GetString("enchantmentSlots") + " " + EnchantmentSlots.ToString() + "\n";

            foreach(IEffect effect in Effects)
            {
                description += effect.GetDescription() + "\n";
            }

            return description;
        }
    }
}
