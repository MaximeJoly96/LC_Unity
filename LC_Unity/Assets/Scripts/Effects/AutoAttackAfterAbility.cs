using Language;

namespace Effects
{
    public class AutoAttackAfterAbility : IEffect
    {
        public string GetDescription()
        {
            return Localizer.Instance.GetString("autoAttackAfterAbilityDescription");
        }
    }
}
