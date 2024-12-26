using Core.Model;
using Abilities;

namespace Inventory
{
    public class Consumable : BaseItem
    {
        public int Priority { get; protected set; }
        public ItemUsability Usability { get; protected set; }
        public AbilityAnimation Animation { get; protected set; }
        public TargetEligibility TargetEligibility { get; protected set; }
        public int Range { get; protected set; }

        public Consumable(ElementIdentifier identifier, int icon, int price, ItemCategory category, ItemUsability usability, int priority, AbilityAnimation animation, TargetEligibility targetEligibility, int range) :
            base(identifier, icon, price, category)
        {
            Priority = priority;
            Usability = usability;
            Animation = animation;
            TargetEligibility = targetEligibility;
            Range = range;
        }
    }
}
