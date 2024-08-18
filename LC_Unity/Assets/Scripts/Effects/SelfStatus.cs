using Actors;
using Language;

namespace Effects
{
    public class SelfStatus : IEffect
    {
        public EffectType Value { get; set; }

        public string GetDescription()
        {
            return Localizer.Instance.GetString("selfStatusDescription") + " " + 
                   Localizer.Instance.GetString(LanguageUtility.GetEffectTypeLanguageKey(Value));
        }
    }
}
