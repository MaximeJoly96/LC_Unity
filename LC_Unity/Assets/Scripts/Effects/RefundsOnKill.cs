using Language;

namespace Effects
{
    public class RefundsOnKill : IEffect
    {
        public Stat Stat { get; set; }
        public float Value { get; set; }

        public string GetDescription()
        {
            return Localizer.Instance.GetString("refundsOnKill") + " " + Value +
                   "%(" + Localizer.Instance.GetString(LanguageUtility.GetStatLanguageKey(Stat)) + ")";
        }
    }
}
