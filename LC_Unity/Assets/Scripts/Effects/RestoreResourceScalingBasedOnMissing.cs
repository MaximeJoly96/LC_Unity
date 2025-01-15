namespace Effects
{
    public class RestoreResourceScalingBasedOnMissing : IEffect
    {
        public Stat Stat { get; set; }
        public float Value { get; set; }

        public string GetDescription()
        {
            return "";
        }
    }
}
