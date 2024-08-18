using Language;

namespace Effects
{
    public class MagicIgnoresDefenseStat : IEffect
    {
        public Stat Stat { get; set; }
        public float Value { get; set; }

        public string GetDescription()
        {
            return Localizer.Instance.GetString("magicIgnoreDefenseStatDescription") + " " +
                   Localizer.Instance.GetString(LanguageUtility.GetStatLanguageKey(Stat)) + " (" + Value.ToString() + ")%";
        }
    }
}
