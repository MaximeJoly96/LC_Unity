namespace Effects
{
    public class RestoreMissingResourceScalingForEachHit : IEffect
    {
        public Stat Stat { get; set; }
        public float Value { get; set; }

        public string GetDescription()
        {
            return "";
        }
    }
}
