using Language;

namespace Effects
{
    public class AdditionalStrike : IEffect
    {
        public int Amount { get; set; }

        public string GetDescription()
        {
            return Localizer.Instance.GetString("additionalStrikeDescription") + " " + Amount;
        }
    }
}
