namespace Effects
{
    public class TransferResourceScalingWithExcessConversion : IEffect
    {
        public float Quantity { get; set; }
        public Stat Stat { get; set; }
        public Stat ConvertedInto { get; set; }
        public int FormulaId { get; set; }

        public string GetDescription()
        {
            return "";
        }
    }
}
