using Language;

namespace Effects
{
    public class DrainFromDamage : IEffect
    {
        public Stat Stat { get; set; }
        public float Value { get; set; }

        public string GetDescription()
        {
            return Localizer.Instance.GetString("drainFromDamageDescription") + " " + Value.ToString() + 
                   "% (" + Localizer.Instance.GetString(LanguageUtility.GetStatLanguageKey(Stat)) + ")";
        }
    }
}
