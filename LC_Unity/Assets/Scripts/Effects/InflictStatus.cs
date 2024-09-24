using Actors;
using Language;
using System.Linq;

namespace Effects
{
    public class InflictStatus : IEffect
    {
        public EffectType Value { get; set; }

        public string GetDescription()
        {
            return Localizer.Instance.GetString("inflictStatusDescription") + " " + 
                   Localizer.Instance.GetString(LanguageUtility.GetEffectTypeLanguageKey(Value));
        }

        public void Apply(Character target)
        {
            if(!target.ActiveEffects.Any(e => e.Effect == Value))
                target.ActiveEffects.Add(new ActiveEffect { Effect = Value });
        }
    }
}
