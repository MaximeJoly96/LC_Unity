using Effects;
using System.Collections.Generic;
using Language;

namespace Inventory
{
    public class EquipmentItem : BaseItem
    {
        public int EnchantmentSlots { get; protected set; }
        public List<IEffect> Effects { get; protected set; }

        public EquipmentItem(int id, string name, string description, int icon, int price, ItemCategory category, int enchantmentSlots) : 
            base(id, name, description, icon, price, category)
        {
            EnchantmentSlots = enchantmentSlots;
            Effects = new List<IEffect>();
        }

        public void AddEffect(IEffect effect)
        {
            Effects.Add(effect);
        }

        public void AddEffects(IEnumerable<IEffect> effects)
        {
            Effects.AddRange(effects);
        }

        public override string DetailedDescription()
        {
            string description = base.DetailedDescription() + "\n";

            description += Localizer.Instance.GetString("enchantmentSlots") + " " + EnchantmentSlots.ToString() + "\n";

            foreach(IEffect effect in Effects)
            {
                description += effect.GetDescription() + "\n";
            }

            return description;
        }
    }
}
