using Actors;

namespace Effects
{
    public class InflictStatus : IEffect
    {
        public EffectType Value { get; set; }

        public string GetDescription()
        {
            return "";
        }
    }
}
