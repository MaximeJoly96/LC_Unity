using Actors;

namespace Effects
{
    public class BonusDamageIfTargetHasStatus : IEffect
    {
        public EffectType Status { get; set; }
        public float Value { get; set; }

        public string GetDescription()
        {
            return "";
        }
    }
}
