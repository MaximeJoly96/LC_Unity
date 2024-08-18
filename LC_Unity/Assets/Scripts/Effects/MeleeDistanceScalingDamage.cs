using Language;

namespace Effects
{
    public class MeleeDistanceScalingDamage : IEffect
    {
        public int DistanceCap { get; set; }
        public float Value { get; set; }

        public string GetDescription()
        {
            return Localizer.Instance.GetString("meleeDistanceScalingDamage");
        }
    }
}
