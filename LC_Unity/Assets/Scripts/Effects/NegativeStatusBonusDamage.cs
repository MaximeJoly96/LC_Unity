using Language;

namespace Effects
{
    public class NegativeStatusBonusDamage : IEffect
    {
        public float Value { get; set; }

        public string GetDescription()
        {
            return Localizer.Instance.GetString("negativeStatusBonusDamage") + " " + Value + "%";
        }
    }
}
