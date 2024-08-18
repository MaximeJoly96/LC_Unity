using Language;
using Abilities;

namespace Effects
{
    public class BoundAbility : IEffect
    {
        public int AbilityId { get; set; }

        public string GetDescription()
        {
            return Localizer.Instance.GetString("boundAbilityDescription") + " " + AbilitiesManager.Instance.GetAbility(AbilityId).Name;
        }
    }
}
