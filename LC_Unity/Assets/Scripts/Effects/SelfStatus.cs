using Actors;

namespace Effects
{
    public class SelfStatus : IEffect
    {
        public EffectType Value { get; set; }

        public string GetDescription()
        {
            return "";
        }
    }
}
