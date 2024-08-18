using Language;

namespace Effects
{
    public class RefillOnParry : IEffect
    {
        public Stat Stat { get; set; }
        public float Value { get; set; }

        public string GetDescription()
        {
            return Localizer.Instance.GetString("refillOnParry") + " " + Value + 
                   "%(" + Localizer.Instance.GetString(LanguageUtility.GetStatLanguageKey(Stat)) + ")";
        }
    }
}
