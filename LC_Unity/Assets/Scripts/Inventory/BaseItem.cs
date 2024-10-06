using Effects;
using Language;
using System.Collections.Generic;
using Core.Model;

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
        protected ElementIdentifier _identifier;
        public int Id { get { return _identifier.Id; } }
        public string Name { get { return _identifier.NameKey; } }
        public string Description { get { return _identifier.DescriptionKey; } }
        public int Icon { get; protected set; }
        public int Price { get; protected set; }
        public ItemCategory Category { get; protected set; }
        public ItemRecipe Recipe { get; set; }
        public List<IEffect> Effects { get; protected set; }

        public BaseItem(ElementIdentifier identifier, int icon, int price, ItemCategory category)
        {
            _identifier = identifier;
            Icon = icon;
            Price = price;
            Category = category;

            Effects = new List<IEffect>();
        }

        public virtual string DetailedDescription()
        {
            string description = Localizer.Instance.GetString(Description) + "\n";

            foreach (IEffect effect in Effects)
            {
                description += effect.GetDescription() + "\n";
            }

            return description;
        }

        public void AddEffect(IEffect effect)
        {
            Effects.Add(effect);
        }

        public void AddEffects(IEnumerable<IEffect> effects)
        {
            Effects.AddRange(effects);
        }
    }
}
