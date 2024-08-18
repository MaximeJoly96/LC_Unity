using Actors;
using Language;

namespace Effects
{
    public class StatusImmunity : IEffect
    {
        public EffectType Value { get; set; }

        public string GetDescription()
        {
            return Localizer.Instance.GetString("statusImmunityDescription") + " " +
                   Localizer.Instance.GetString(LanguageUtility.GetEffectTypeLanguageKey(Value));
        }
    }
}
