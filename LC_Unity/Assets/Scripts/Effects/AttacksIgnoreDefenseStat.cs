using Language;

namespace Effects
{
    public class AttacksIgnoreDefenseStat : IEffect
    {
        public Stat Stat { get; set; }
        public float Value { get; set; }

        public string GetDescription()
        {
            return Localizer.Instance.GetString("attacksIgnoreDefenseStatDescription") + " " +
                   Localizer.Instance.GetString(LanguageUtility.GetStatLanguageKey(Stat)) + " (" + Value.ToString() + ")%";
        }
    }
}
