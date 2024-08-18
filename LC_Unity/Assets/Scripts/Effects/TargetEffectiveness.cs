using Language;

namespace Effects
{
    public enum TargetTribe
    {
        Human,
        Undead
    }

    public class TargetEffectiveness : IEffect
    {
        public TargetTribe Type { get; set; }
        public float Value { get; set; }

        public string GetDescription()
        {
            return Localizer.Instance.GetString("targetEffectiveness") + " " + 
                   Localizer.Instance.GetString(LanguageUtility.GetTribeLanguageKey(Type)) + "(" + Value + "%)";
        }
    }
}
