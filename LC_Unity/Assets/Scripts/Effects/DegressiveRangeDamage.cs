using Language;

namespace Effects
{
    public class DegressiveRangeDamage : IEffect
    {
        public float MinDamage { get; set; }

        public string GetDescription()
        {
            return Localizer.Instance.GetString("degressiveRangeDamage") + " " + MinDamage.ToString() + "%)";
        }
    }
}
