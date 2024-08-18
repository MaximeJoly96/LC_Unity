using Language;

namespace Effects
{
    public class HpThresholdBonusDamage : IEffect
    {
        public float Threshold {  get; set; }

        public string GetDescription()
        {
            return Localizer.Instance.GetString("hpThresholdBonusDamage") + " " + Threshold + "%";
        }
    }
}
