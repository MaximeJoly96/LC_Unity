using Language;

namespace Effects
{
    public class CostReduction : IEffect
    {
        public Stat Stat { get; set; }
        public float Value { get; set; }

        public string GetDescription()
        {
            return Localizer.Instance.GetString("costReductionDescription") + " " + 
                   Value.ToString() + "% (" + Localizer.Instance.GetString(LanguageUtility.GetStatLanguageKey(Stat)) + ")"; 
        }
    }
}
