using Actors;

namespace Effects
{
    public class InflictStatusAroundTarget : IEffect
    {
        public enum Eligibility { Allies, Enemies, All }

        public Eligibility Eligible { get; set; }
        public EffectType Value { get; set; }

        public string GetDescription()
        {
            return "";
        }
    }
}
