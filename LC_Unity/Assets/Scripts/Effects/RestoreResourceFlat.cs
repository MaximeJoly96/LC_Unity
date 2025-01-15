namespace Effects
{
    public class RestoreResourceFlat : IEffect
    {
        public Stat Stat { get; set; }
        public float Value { get; set; }

        public string GetDescription()
        {
            return "";
        }
    }
}
