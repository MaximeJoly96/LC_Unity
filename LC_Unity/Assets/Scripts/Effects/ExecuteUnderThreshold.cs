namespace Effects
{
    public class ExecuteUnderThreshold : IEffect
    {
        public Stat Stat { get; set; }
        public float Threshold { get; set; }

        public string GetDescription()
        {
            return "";
        }
    }
}
