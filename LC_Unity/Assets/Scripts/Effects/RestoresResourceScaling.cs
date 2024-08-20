using Language;

namespace Effects
{
    public class RestoresResourceScaling : IEffect
    {
        public Stat Stat { get; set; }
        public float Value { get; set; }

        public string GetDescription()
        {
            return Localizer.Instance.GetString("restoresResourceScaling") + " " + Value + "(" +
                   Localizer.Instance.GetString(LanguageUtility.GetStatLanguageKey(Stat)) + ")";
        }
    }
}
