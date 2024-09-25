using Actors;
using Language;
using System.Linq;

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

        public void Apply(Character character)
        {
            ActiveEffect activeEffect = character.ActiveEffects.FirstOrDefault(e => e.Effect == Value);
            
            if(activeEffect != null)
                character.ActiveEffects.Remove(activeEffect);
        }
    }
}
