using Actors;
using Language;

namespace Effects
{
    public class Dispel : IEffect
    {
        public EffectType Value { get; set; }

        public string GetDescription()
        {
            return Localizer.Instance.GetString("dispelDescription") + " " + 
                   Localizer.Instance.GetString(LanguageUtility.GetEffectTypeLanguageKey(Value));
        }
    }
}
