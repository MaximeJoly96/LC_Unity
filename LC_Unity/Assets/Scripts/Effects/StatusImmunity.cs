using Actors;

namespace Effects
{
    public class StatusImmunity : IEffect
    {
        public EffectType Value { get; set; }

        public string GetDescription()
        {
            return "";
        }
    }
}
