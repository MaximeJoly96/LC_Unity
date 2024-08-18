using Language;

namespace Effects
{
    public class BonusDamageToShields : IEffect
    {
        public float Value { get; set; }

        public string GetDescription()
        {
            return Localizer.Instance.GetString("bonusDamageToShieldsDescription") + " " + Value.ToString();
        }
    }
}
