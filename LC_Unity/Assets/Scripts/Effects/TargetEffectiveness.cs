namespace Effects
{
    public enum TargetTribe
    {
        Human,
        Undead
    }

    public class TargetEffectiveness : IEffect
    {
        public TargetTribe Type { get; set; }
        public float Value { get; set; }
    }
}
