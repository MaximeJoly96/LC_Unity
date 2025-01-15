namespace Effects
{
    public class RestoreStatOnKill : IEffect
    {
        public Stat Stat { get; set; }
        public int FormulaId { get; set; }

        public string GetDescription()
        {
            return "";
        }
    }
}
