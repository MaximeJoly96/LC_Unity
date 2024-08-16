using Actors;

namespace Effects
{
    public class AreaOfEffectAsSecondaryDamage : IEffect
    {
        public Element Element { get; set; }
        public int BaseDamage { get; set; }
    }
}
