using Language;

namespace Effects
{
    public class CastPositiveStatusDurationExtension : IEffect
    {
        public int Turns { get; set; }

        public string GetDescription()
        {
            string modifier = Turns > 0 ? "+" + Turns : Turns.ToString();

            return Localizer.Instance.GetString("castPositiveStatusDurationExtensionDescription") + " " + modifier;
        }
    }
}
